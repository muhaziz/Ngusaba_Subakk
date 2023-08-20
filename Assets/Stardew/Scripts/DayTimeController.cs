using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public enum DayOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

public class DayTimeController : MonoBehaviour
{
    const float secondsInDays = 86400f;
    const float phaseLength = 900f;

    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color sunRiseLightColor = new Color(1f, 0.75f, 0.5f); // Contoh warna fajar
    [SerializeField] Color nightLightColor = new Color(0.1f, 0.1f, 0.2f); // Contoh warna malam
    [SerializeField] Color sunSetLightColor = new Color(1f, 0.4f, 0.2f); // Contoh warna senja
    [SerializeField] Color dayLightColor = Color.white; // Contoh warna siang

    [SerializeField] TextMeshProUGUI dayOfTheWeekText; // Referensi ke objek TextMeshPro UI untuk nama hari
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] Light2D globalLight;
    [SerializeField] Light2D myLight;

    private string targetObjectName = "Lampu";
    public GameObject targetObject;


    string[] dayNames = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
    int currentDayIndex = 0;
    private int days;
    private int currentDate = 1;
    private float time;
    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f;


    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }
    private void Start()
    {
        targetObject = GameObject.Find(targetObjectName);
        if (targetObject != null)
        {
            Debug.Log("Lampu ditemukan!");
        }
        else
        {
            Debug.Log("Lampu tidak ditemukan!");
        }
        time = startAtTime;
    }


    public void Subscribe(TimeAgent timeAgent)
    {
        if (!agents.Contains(timeAgent))
        {
            agents.Add(timeAgent);
        }
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        if (agents.Contains(timeAgent))
        {
            agents.Remove(timeAgent);
        }
    }

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }


    void Update()
    {
        time += Time.deltaTime * timeScale;
        TimeValueCalculation();
        DayLight();

        if (time > secondsInDays)
        {
            nextDay();
        }
        TimeAgents();
        ControlGameObjectBasedOnTime();
    }

    private void ControlGameObjectBasedOnTime()
    {
        float currentTime = Hours + Minutes / 60f;
        if (currentTime >= 18f && currentTime < 6f) // Jika saat ini antara jam 18:00 hingga 6:00
        {
            if (targetObject != null)
                targetObject.SetActive(true); // Aktifkan GameObject
        }
        else
        {
            if (targetObject != null)
                targetObject.SetActive(false); // Nonaktifkan GameObject
        }
    }

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLength);
        // Debug.Log(currentPhase);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke(this);
            }
        }
    }
    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c;
        if (v < 0.25f)
        {
            c = Color.Lerp(nightLightColor, sunRiseLightColor, v / 0.25f);
        }
        else if (v < 0.75f)
        {
            c = Color.Lerp(sunRiseLightColor, sunSetLightColor, (v - 0.25f) / 0.5f);
        }
        else
        {
            c = dayLightColor;
        }
        globalLight.color = c;

        float currentTime = Hours + Minutes / 60f;
        if (currentTime >= 17.5f || currentTime < 6f) // memeriksa apakah waktu sekarang setelah jam 17:30 atau sebelum jam 6:00
        {
            myLight.enabled = true; // mengaktifkan lampu
        }
        else
        {
            myLight.enabled = false; // menonaktifkan lampu
        }
    }

    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        if (mm % 5 != 0)
        {
            mm = ((mm / 5) + 1) * 5; // Menjadikan menit kelipatan 5 berikutnya
            if (mm >= 60)
            {
                mm = 0; // Reset menit ke 0 jika sudah mencapai 60
                hh = (hh + 1) % 24; // Tambah 1 jam dan reset ke 0 setelah mencapai 24 jam
            }
        }
        dayOfTheWeekText.text = hh.ToString("00") + ":" + mm.ToString("00");

        // Tampilkan nama hari sekarang di UI
        int currentDay = (days + currentDayIndex) % 7; // Hitung indeks hari saat ini
        dateText.text = dayNames[currentDay] + " , " + currentDate.ToString("00");
    }

    private void nextDay()
    {
        time = 0;
        days += 1;

        // Reset tanggal ke 1 jika tanggal melebihi 30
        if (currentDate >= 30)
        {
            currentDate = 1;
        }
        else
        {
            currentDate++;
        }
    }

    public void SleepUntilMorning()
    {
        time = 6f * 3600f; // Mengatur waktu menjadi 06:00
        myLight.enabled = false; // Mematikan lampu (jika lampu harus mati saat tidur)
    }
    public void SkipToMorning()
    {
        // Ubah nilai time ke waktu pagi
        time = 6f * 3600f; // Mengatur waktu menjadi 06:00
    }
}
