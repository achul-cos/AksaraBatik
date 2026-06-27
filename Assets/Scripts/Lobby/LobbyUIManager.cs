using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LobbyUIManager : MonoBehaviour
{
    // Debug Sub Panel UI Component

    [Header("Debug Sub Panel UI Component")]
    [SerializeField] private GameObject _debugSubPanel;
    [SerializeField] private TextMeshProUGUI _phaseNameDebug;
    [SerializeField] private TextMeshProUGUI _phaseIndexDebug;
    [SerializeField] private TextMeshProUGUI _dayNameDebug;
    [SerializeField] private TextMeshProUGUI _dayIndexDebug;
    [SerializeField] private TextMeshProUGUI _customersDayDebug;
    [SerializeField] private TextMeshProUGUI _customersQueueDebug;
    [SerializeField] private TextMeshProUGUI _customersServedDebug;
    [SerializeField] private TextMeshProUGUI _endDayHourDebug;
    [SerializeField] private TextMeshProUGUI _startDayHourDebug;
    [SerializeField] private TextMeshProUGUI _currentHourDebug;
    [SerializeField] private TextMeshProUGUI _currentMinuteDebug;
    [SerializeField] private TextMeshProUGUI _currentTimeMinuteDebug;
    [SerializeField] private TextMeshProUGUI _currentBalanceDebug;
    [SerializeField] private TextMeshProUGUI _currentWeatherDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedIDDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedNameDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedWealthDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedPerfectDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedBatikNameDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedBatikColorDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedFabricNameDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedFabricPriceDebug;
    [SerializeField] private TextMeshProUGUI _currentCustomerServedBatikPriceDebug;
    [SerializeField] private TextMeshProUGUI _customersMiskinChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersBiasaChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersKayaChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersSultanChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersPerfectDChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersPerfectCChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersPerfectBChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersPerfectAChanceDebug;
    [SerializeField] private TextMeshProUGUI _customersPerfectSChanceDebug;
    [SerializeField] private TextMeshProUGUI _targetCustomersDebug;
    [SerializeField] private TextMeshProUGUI _targetBalanceDebug;
    [SerializeField] private TextMeshProUGUI _nextDayDebug;
    [SerializeField] private TextMeshProUGUI _nextPhaseDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue1NameDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue1WealthDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue1PerfectDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue2NameDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue2WealthDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue2PerfectDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue3NameDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue3WealthDebug;
    [SerializeField] private TextMeshProUGUI _customerQueue3PerfectDebug;

    // Debug Sub Panel Variable

    private void Update()
    {
        if(_debugSubPanel.activeSelf == true) LoadDebugSubPanel();
        HandleDebug();
    }

    private void HandleDebug()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (_debugSubPanel.activeSelf == false) _debugSubPanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F5))
        {
            if (_debugSubPanel.activeSelf == true) _debugSubPanel.SetActive(false);
        }
    }

    public void LoadDebugSubPanel()
    {
        PhaseConfig phaseConfig = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(GameManager.Instance.CurrentPhase);
        DayConfig dayConfig = GameManager.Instance.PhasesConfigDatabase.GetDayConfig(GameManager.Instance.CurentDay);

        float totalChance = (dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.MISKIN)?.wealthChance ?? 0f) + (dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.BIASA)?.wealthChance ?? 0f) + (dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.KAYA)?.wealthChance ?? 0f) + (dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.SULTAN)?.wealthChance ?? 0f);
        float totalPerfect = (dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.D)?.perfectChance ?? 0f) + (dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.C)?.perfectChance ?? 0f) + (dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.B)?.perfectChance ?? 0f) + (dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.A)?.perfectChance ?? 0f) + (dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.S)?.perfectChance ?? 0f);

        _phaseNameDebug.text = $"Phase Name : {phaseConfig?.phaseName ?? "null"}";
        _phaseIndexDebug.text = $"Phase Index : {GameManager.Instance.CurrentPhase.ToString() ?? "null"}";
        _dayNameDebug.text = $"Day Name : {dayConfig?.dayName ?? "null"}";
        _dayIndexDebug.text = $"Day Index : {GameManager.Instance.CurentDay.ToString() ?? "null"}";
        _customersDayDebug.text = $"Customers Day : {dayConfig?.dayCustomers.ToString() ?? "null"}";
        _customersQueueDebug.text = $"Customers Queue : {CustomerManager.Instance.CustomerQueueCount.ToString() ?? "null"}";
        _customersServedDebug.text = $"Customers Served : {CustomerManager.Instance.CustomerServedToday.ToString() ?? "null"}";
        _endDayHourDebug.text = $"End Day Hour : {TimeManager.Instance.EndHour.ToString() ?? "null"}";
        _startDayHourDebug.text = $"Start Day Hour : {TimeManager.Instance.StartHour.ToString() ?? "null"}";
        _currentHourDebug.text = $"Current Hour : {TimeManager.Instance.GetCurrentHour().ToString() ?? "null"}";
        _currentMinuteDebug.text = $"Current Minute : {TimeManager.Instance.GetCurrentMinute().ToString() ?? "null"}";
        _currentTimeMinuteDebug.text = $"Current Time Minute : {TimeManager.Instance.TimeMinute.ToString() ?? "null"}";
        _currentBalanceDebug.text = $"Current Balance : {GameManager.Instance.CurrentBalance.ToString() ?? "null"}";
        _currentWeatherDebug.text = $"Current Weather : {dayConfig.dayWeather.ToString() ?? "null"}";
        _currentCustomerServedIDDebug.text = $"Customers Served ID : {CustomerManager.Instance.CustomerCurrent?.customerId ?? "null"}";
        _currentCustomerServedNameDebug.text = $"C.S Name : {CustomerManager.Instance.CustomerCurrent?.customerName ?? "null"}";
        _currentCustomerServedWealthDebug.text = $"C.S Wealth : {CustomerManager.Instance.CustomerCurrent?.customerWealth.ToString() ?? "null"}";
        _currentCustomerServedPerfectDebug.text = $"C.S Perfect : {CustomerManager.Instance.CustomerCurrent?.customerPerfect.ToString() ?? "null"}";
        _currentCustomerServedBatikNameDebug.text = $"C.S Batik Name : {CustomerManager.Instance.CustomerCurrent?.customerBatik.batikName ?? "null"}";
        _currentCustomerServedBatikColorDebug.text = $"C.S Batik Color : {CustomerManager.Instance.CustomerCurrent?.customerBatikColor.ToString() ?? "null"}";
        _currentCustomerServedFabricNameDebug.text = $"C.S Fabric Name : {CustomerManager.Instance.CustomerCurrent?.customerFabric.fabricName ?? "null"}";
        _currentCustomerServedFabricPriceDebug.text = $"C.S Fabric Price : {CustomerManager.Instance.CustomerCurrent?.customerFabric.fabricPrice.ToString() ?? "null"}";
        _currentCustomerServedBatikPriceDebug.text = $"C.S Batik Price : {CustomerManager.Instance.CustomerCurrent?.customerBatik.batikPrice.ToString() ?? "null"}";
        _customersMiskinChanceDebug.text = $"Cust. Miskin Chance : {(dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.MISKIN)?.wealthChance / totalChance).ToString() ?? "null"}";
        _customersBiasaChanceDebug.text = $"Cust. Biasa Chance : {(dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.BIASA)?.wealthChance / totalChance).ToString() ?? "null"}";
        _customersKayaChanceDebug.text = $"Cust. Kaya Chance : {(dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.KAYA)?.wealthChance / totalChance).ToString() ?? "null"}";
        _customersSultanChanceDebug.text = $"Cust. Sultan Chance : {(dayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.SULTAN)?.wealthChance / totalChance).ToString() ?? "null"}";
        _customersPerfectDChanceDebug.text = $"Cust. Perfect D Chance : {(dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.D)?.perfectChance / totalPerfect).ToString() ?? "null"}";
        _customersPerfectCChanceDebug.text = $"Cust. Perfect C Chance : {(dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.C)?.perfectChance / totalPerfect).ToString() ?? "null"}";
        _customersPerfectBChanceDebug.text = $"Cust. Perfect B Chance : {(dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.B)?.perfectChance / totalPerfect).ToString() ?? "null"}";
        _customersPerfectAChanceDebug.text = $"Cust. Perfect A Chance : {(dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.A)?.perfectChance / totalPerfect).ToString() ?? "null"}";
        _customersPerfectSChanceDebug.text = $"Cust. Perfect S Chance : {(dayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.S)?.perfectChance / totalPerfect).ToString() ?? "null"}";
        _targetCustomersDebug.text = $"Target Customers : {phaseConfig.phaseTarget.FirstOrDefault(x => x.targetType == TargetType.Customers)?.targetValue.ToString() ?? "null"}";
        _targetBalanceDebug.text = $"Target Balance : {phaseConfig.phaseTarget.FirstOrDefault(x => x.targetType == TargetType.Income)?.targetValue.ToString() ?? "null"}";
        _nextDayDebug.text = $"Next Day : {GameManager.Instance.GetNextDayConfig().dayName ?? "null"}";
        _nextPhaseDebug.text = $"Next Phase : {GameManager.Instance.GetNextPhaseConfig().phaseName ?? "Selesai"}";
        _customerQueue1NameDebug.text = $"Cs 1 Name : {CustomerManager.Instance.PeekCustomerAtIndex(0)?.customerName ?? "null"} ; ID : {CustomerManager.Instance.PeekCustomerAtIndex(0)?.customerId.ToString() ?? "null"}";
        _customerQueue1WealthDebug.text = $"Cs 1 Wealth : {CustomerManager.Instance.PeekCustomerAtIndex(0)?.customerWealth.ToString() ?? "null"}";
        _customerQueue1PerfectDebug.text = $"Cs 1 Perfect : {CustomerManager.Instance.PeekCustomerAtIndex(0)?.customerPerfect.ToString() ?? "null"}";
        _customerQueue2NameDebug.text = $"Cs 2 Name : {CustomerManager.Instance.PeekCustomerAtIndex(1)?.customerName ?? "null"} ; ID : {CustomerManager.Instance.PeekCustomerAtIndex(1)?.customerId.ToString() ?? "null"}";
        _customerQueue2WealthDebug.text = $"Cs 2 Wealth : {CustomerManager.Instance.PeekCustomerAtIndex(1)?.customerWealth.ToString() ?? "null"}";
        _customerQueue2PerfectDebug.text = $"Cs 2 Perfect : {CustomerManager.Instance.PeekCustomerAtIndex(1)?.customerPerfect.ToString() ?? "null"}";
        _customerQueue3NameDebug.text = $"Cs 3 Name : {CustomerManager.Instance.PeekCustomerAtIndex(2)?.customerName ?? "null"} ; ID : {CustomerManager.Instance.PeekCustomerAtIndex(2)?.customerId.ToString() ?? "null"}";
        _customerQueue3WealthDebug.text = $"Cs 3 Wealth : {CustomerManager.Instance.PeekCustomerAtIndex(2)?.customerWealth.ToString() ?? "null"}";
        _customerQueue3PerfectDebug.text = $"Cs 3 Perfect : {CustomerManager.Instance.PeekCustomerAtIndex(2)?.customerPerfect.ToString() ?? "null"}";
    }

    private void Start()
    {
        _debugSubPanel.SetActive(false);
    }
}
