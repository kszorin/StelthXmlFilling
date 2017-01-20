using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

/*
    Класс, описывающий главную форму и поведение программы.
    При добавлении новой лицензии, класс требуется редактировать.
*/

namespace StelthXmlFilling
{
    public partial class mainForm : Form
    {
        private XDocument etalonXml;
        private const String ETALON_XML_NAME = "etalon.xml";
        private XDocument newXml = null;
        private String newXmlName;
        private XDocument readmeXml = null;
        private const String LPMAKER_NAME = "lpmaker-wrapper.cmd";
        private const String GDSTOOL_NAME = "gdstool.exe";
        private const String MAIN_KEY_TEMP_INIT_FILE_NAME = "mainKeyTempInitFileName.txt";
        private const String RESERVE_KEY_TEMP_INIT_FILE_NAME = "reserveKeyTempInitFileName.txt";
        private const String CERTIFICATE_FILE_NAME = "certificate.cert";
        private const String DEFAULT_DATE = "09.05.1945";
        private const String ERROR_UNKNOWN_KEY_NUM = "Неизвестен номер одного из ключей!";
        private const String MAIN_TITLE = "Stelth III XML Filling - ";
        private const String INSERT_MAIN_TITLE = "Вставьте основной ключ.";
        private const String INSERT_RESERVE_TITLE = "Вставьте резервный ключ.";
        private const String ERROR_TITLE = "Ошибка!";
        private const String ERROR_UNKNOWN_CITY = "Введите название города!";
        private const String ERROR_UNKNOWN_DEPARTMENT = "Введите название ведомства!";
        private const String ERROR_UNKNOWN_MAIN_KEY_NUM = "Введите номер основного ключа!";
        private const String ERROR_UNKNOWN_MAIN_KEY_ID = "Введите id основного ключа!";
        private const String ERROR_UNKNOWN_MAIN_KEY_CODE = "Введите код основного ключа!";
        private const String ERROR_UNKNOWN_RESERVE_KEY_NUM = "Введите номер резервного ключа!";
        private const String ERROR_UNKNOWN_RESERVE_KEY_ID = "Введите id резервного ключа!";
        private const String ERROR_UNKNOWN_RESERVE_KEY_CODE = "Введите код резервного ключа!";
        private const String ERROR_NO_DATA_KEYS = "Введите данные хотя бы для одного ключа!";
        private const String RESULT_FORM_TITLE = "Состояние лицензий";
        private String resultText="";
        private StelthXml licenceInfo;

        public mainForm()
        {
            InitializeComponent();

            //загрузка эталонного файла
            etalonXml = XDocument.Load(ETALON_XML_NAME);

            licenceInfo = new StelthXml();
            BindingFields(licenceInfo);
            DateboxInit();
        }

        #region СОЗДАНИЕ НОВОГО ФАЙЛА
        //связывание между собой полей в форме со свойствами класса StelthXmlLicence
        private void BindingFields(StelthXml licenceInfo)
        {
            licenceInfo.licences[0].ConstantAmountTextbox = magreadConstantAmountTextbox;
            licenceInfo.licences[0].TempAmountTextbox = magreadTempAmountTextbox;
            licenceInfo.licences[0].TempDatebox = magreadTempDatebox;

            /*licenceInfo.licences[0].group = magreadGroup;
            licenceInfo.licences[0].group.Controls.*/

            licenceInfo.licences[1].ConstantAmountTextbox = taskjournalConstantAmountTextbox;
            licenceInfo.licences[1].TempAmountTextbox = taskjournalTempAmountTextbox;
            licenceInfo.licences[1].TempDatebox = taskjournalTempDatebox;

            licenceInfo.licences[2].ConstantAmountTextbox = magfaxConstantAmountTextbox;
            licenceInfo.licences[2].TempAmountTextbox = magfaxTempAmountTextbox;
            licenceInfo.licences[2].TempDatebox = magfaxTempDatebox;

            licenceInfo.licences[3].ConstantAmountTextbox = trackingConstantAmountTextbox;
            licenceInfo.licences[3].TempAmountTextbox = trackingTempAmountTextbox;
            licenceInfo.licences[3].TempDatebox = trackingTempDatebox;

            licenceInfo.licences[4].ConstantAmountTextbox = audioplayerConstantAmountTextbox;
            licenceInfo.licences[4].TempAmountTextbox = audioplayerTempAmountTextbox;
            licenceInfo.licences[4].TempDatebox = audioplayerTempDatebox;

            licenceInfo.licences[5].ConstantAmountTextbox = billclientConstantAmountTextbox;
            licenceInfo.licences[5].TempAmountTextbox = billclientTempAmountTextbox;
            licenceInfo.licences[5].TempDatebox = billclientTempDatebox;

            licenceInfo.licences[6].ConstantAmountTextbox = mwieserverConstantAmountTextbox;
            licenceInfo.licences[6].TempAmountTextbox = mwieserverTempAmountTextbox;
            licenceInfo.licences[6].TempDatebox = mwieserverTempDatebox;

            licenceInfo.licences[7].ConstantAmountTextbox = mwieclientConstantAmountTextbox;
            licenceInfo.licences[7].TempAmountTextbox = mwieclientTempAmountTextbox;
            licenceInfo.licences[7].TempDatebox = mwieclientTempDatebox;

            licenceInfo.licences[8].ConstantAmountTextbox = mwiestationConstantAmountTextbox;
            licenceInfo.licences[8].TempAmountTextbox = mwiestationTempAmountTextbox;
            licenceInfo.licences[8].TempDatebox = mwiestationTempDatebox;

            licenceInfo.licences[9].ConstantAmountTextbox = integrationserverConstantAmountTextbox;
            licenceInfo.licences[9].TempAmountTextbox = integrationserverTempAmountTextbox;
            licenceInfo.licences[9].TempDatebox = integrationserverTempDatebox;

            licenceInfo.licences[10].ConstantAmountTextbox = integrationadapterConstantAmountTextbox;
            licenceInfo.licences[10].TempAmountTextbox = integrationadapterTempAmountTextbox;
            licenceInfo.licences[10].TempDatebox = integrationadapterTempDatebox;

            licenceInfo.licences[11].ConstantAmountTextbox = videojournalConstantAmountTextbox;
            licenceInfo.licences[11].TempAmountTextbox = videojournalTempAmountTextbox;
            licenceInfo.licences[11].TempDatebox = videojournalTempDatebox;

            licenceInfo.licences[12].ConstantAmountTextbox = imitatorsormConstantAmountTextbox;
            licenceInfo.licences[12].TempAmountTextbox = imitatorsormTempAmountTextbox;
            licenceInfo.licences[12].TempDatebox = imitatorsormTempDatebox;

            licenceInfo.licences[13].ConstantAmountTextbox = maggradientConstantAmountTextbox;
            licenceInfo.licences[13].TempAmountTextbox = maggradientTempAmountTextbox;
            licenceInfo.licences[13].TempDatebox = maggradientTempDatebox;

            licenceInfo.licences[14].ConstantAmountTextbox = tracking3ConstantAmountTextbox;
            licenceInfo.licences[14].TempAmountTextbox = tracking3TempAmountTextbox;
            licenceInfo.licences[14].TempDatebox = tracking3TempDatebox;

            licenceInfo.licences[15].ConstantAmountTextbox = gatemegafonConstantAmountTextbox;
            licenceInfo.licences[15].TempAmountTextbox = gatemegafonTempAmountTextbox;
            licenceInfo.licences[15].TempDatebox = gatemegafonTempDatebox;

            licenceInfo.licences[16].ConstantAmountTextbox = gatemtsConstantAmountTextbox;
            licenceInfo.licences[16].TempAmountTextbox = gatemtsTempAmountTextbox;
            licenceInfo.licences[16].TempDatebox = gatemtsTempDatebox;

            licenceInfo.licences[17].ConstantAmountTextbox = gatebeelineConstantAmountTextbox;
            licenceInfo.licences[17].TempAmountTextbox = gatebeelineTempAmountTextbox;
            licenceInfo.licences[17].TempDatebox = gatebeelineTempDatebox;

            licenceInfo.licences[18].ConstantAmountTextbox = istokclientConstantAmountTextbox;
            licenceInfo.licences[18].TempAmountTextbox = istokclientTempAmountTextbox;
            licenceInfo.licences[18].TempDatebox = istokclientTempDatebox;

            licenceInfo.licences[19].ConstantAmountTextbox = higinaconverterConstantAmountTextbox;
            licenceInfo.licences[19].TempAmountTextbox = higinaconverterTempAmountTextbox;
            licenceInfo.licences[19].TempDatebox = higinaconverterTempDatebox;

            licenceInfo.licences[20].ConstantAmountTextbox = webmapsConstantAmountTextbox;
            licenceInfo.licences[20].TempAmountTextbox = webmapsTempAmountTextbox;
            licenceInfo.licences[20].TempDatebox = webmapsTempDatebox;

            licenceInfo.licences[21].ConstantAmountTextbox = gateyanvarConstantAmountTextbox;
            licenceInfo.licences[21].TempAmountTextbox = gateyanvarTempAmountTextbox;
            licenceInfo.licences[21].TempDatebox = gateyanvarTempDatebox;

            licenceInfo.licences[22].ConstantAmountTextbox = play3gvideoConstantAmountTextbox;
            licenceInfo.licences[22].TempAmountTextbox = play3gvideoTempAmountTextbox;
            licenceInfo.licences[22].TempDatebox = play3gvideoTempDatebox;

            licenceInfo.licences[23].ConstantAmountTextbox = integrationnetconfigConstantAmountTextbox;
            licenceInfo.licences[23].TempAmountTextbox = integrationnetconfigTempAmountTextbox;
            licenceInfo.licences[23].TempDatebox = integrationnetconfigTempDatebox;

            licenceInfo.licences[24].ConstantAmountTextbox = trackingg2ConstantAmountTextbox;
            licenceInfo.licences[24].TempAmountTextbox = trackingg2TempAmountTextbox;
            licenceInfo.licences[24].TempDatebox = trackingg2TempDatebox;
            licenceInfo.licences[24].ConstantHelpdeskEndDatebox = trackingg2ConstantHelpdeskEndDatebox;
            licenceInfo.licences[24].ConstantGuaranteeEndDatebox = trackingg2ConstantGuaranteeEndDatebox;
            licenceInfo.licences[24].TempHelpdeskEndDatebox = trackingg2TempHelpdeskEndDatebox;
            licenceInfo.licences[24].TempGuaranteeEndDatebox = trackingg2TempGuaranteeEndDatebox;
            licenceInfo.licences[24].ConstantAmountTotal = trackingg2ConstantAmountTotal;
            licenceInfo.licences[24].TempAmountTotal = trackingg2TempAmountTotal;
            licenceInfo.licences[24].HistoryBtn = trackingg2HistoryBtn;


            licenceInfo.licences[25].ConstantAmountTextbox = webmapsg2ConstantAmountTextbox;
            licenceInfo.licences[25].TempAmountTextbox = webmapsg2TempAmountTextbox;
            licenceInfo.licences[25].TempDatebox = webmapsg2TempDatebox;
            licenceInfo.licences[25].ConstantHelpdeskEndDatebox = webmapsg2ConstantHelpdeskEndDatebox;
            licenceInfo.licences[25].ConstantGuaranteeEndDatebox = webmapsg2ConstantGuaranteeEndDatebox;
            licenceInfo.licences[25].TempHelpdeskEndDatebox = webmapsg2TempHelpdeskEndDatebox;
            licenceInfo.licences[25].TempGuaranteeEndDatebox = webmapsg2TempGuaranteeEndDatebox;
            licenceInfo.licences[25].ConstantAmountTotal = webmapsg2ConstantAmountTotal;
            licenceInfo.licences[25].TempAmountTotal = webmapsg2TempAmountTotal;
            licenceInfo.licences[25].HistoryBtn = webmapsg2HistoryBtn;

            licenceInfo.licences[26].ConstantAmountTextbox = magreadg2ConstantAmountTextbox;
            licenceInfo.licences[26].TempAmountTextbox = magreadg2TempAmountTextbox;
            licenceInfo.licences[26].TempDatebox = magreadg2TempDatebox;
            licenceInfo.licences[26].ConstantHelpdeskEndDatebox = magreadg2ConstantHelpdeskEndDatebox;
            licenceInfo.licences[26].ConstantGuaranteeEndDatebox = magreadg2ConstantGuaranteeEndDatebox;
            licenceInfo.licences[26].TempHelpdeskEndDatebox = magreadg2TempHelpdeskEndDatebox;
            licenceInfo.licences[26].TempGuaranteeEndDatebox = magreadg2TempGuaranteeEndDatebox;
            licenceInfo.licences[26].ConstantAmountTotal = magreadg2ConstantAmountTotal;
            licenceInfo.licences[26].TempAmountTotal = magreadg2TempAmountTotal;
            licenceInfo.licences[26].HistoryBtn = magreadg2HistoryBtn;

            licenceInfo.licences[27].ConstantAmountTextbox = videojournalg2ConstantAmountTextbox;
            licenceInfo.licences[27].TempAmountTextbox = videojournalg2TempAmountTextbox;
            licenceInfo.licences[27].TempDatebox = videojournalg2TempDatebox;
            licenceInfo.licences[27].ConstantHelpdeskEndDatebox = videojournalg2ConstantHelpdeskEndDatebox;
            licenceInfo.licences[27].ConstantGuaranteeEndDatebox = videojournalg2ConstantGuaranteeEndDatebox;
            licenceInfo.licences[27].TempHelpdeskEndDatebox = videojournalg2TempHelpdeskEndDatebox;
            licenceInfo.licences[27].TempGuaranteeEndDatebox = videojournalg2TempGuaranteeEndDatebox;
            licenceInfo.licences[27].ConstantAmountTotal = videojournalg2ConstantAmountTotal;
            licenceInfo.licences[27].TempAmountTotal = videojournalg2TempAmountTotal;
            licenceInfo.licences[27].HistoryBtn = videojournalg2HistoryBtn;

            licenceInfo.licences[28].ConstantAmountTextbox = gateutkConstantAmountTextbox;
            licenceInfo.licences[28].TempAmountTextbox = gateutkTempAmountTextbox;
            licenceInfo.licences[28].TempDatebox = gateutkTempDatebox;

            licenceInfo.licences[29].ConstantAmountTextbox = smsserviceConstantAmountTextbox;
            licenceInfo.licences[29].TempAmountTextbox = smsserviceTempAmountTextbox;
            licenceInfo.licences[29].TempDatebox = smsserviceTempDatebox;
            licenceInfo.licences[29].ConstantHelpdeskEndDatebox = smsserviceConstantHelpdeskEndDatebox;
            licenceInfo.licences[29].ConstantGuaranteeEndDatebox = smsserviceConstantGuaranteeEndDatebox;
            licenceInfo.licences[29].TempHelpdeskEndDatebox = smsserviceTempHelpdeskEndDatebox;
            licenceInfo.licences[29].TempGuaranteeEndDatebox = smsserviceTempGuaranteeEndDatebox;
            licenceInfo.licences[29].ConstantAmountTotal = smsserviceConstantAmountTotal;
            licenceInfo.licences[29].TempAmountTotal = smsserviceTempAmountTotal;
            licenceInfo.licences[29].HistoryBtn = smsserviceHistoryBtn;

            licenceInfo.licences[30].ConstantAmountTextbox = simserviceConstantAmountTextbox;
            licenceInfo.licences[30].TempAmountTextbox = simserviceTempAmountTextbox;
            licenceInfo.licences[30].TempDatebox = simserviceTempDatebox;
            licenceInfo.licences[30].ConstantHelpdeskEndDatebox = simserviceConstantHelpdeskEndDatebox;
            licenceInfo.licences[30].ConstantGuaranteeEndDatebox = simserviceConstantGuaranteeEndDatebox;
            licenceInfo.licences[30].TempHelpdeskEndDatebox = simserviceTempHelpdeskEndDatebox;
            licenceInfo.licences[30].TempGuaranteeEndDatebox = simserviceTempGuaranteeEndDatebox;
            licenceInfo.licences[30].ConstantAmountTotal = simserviceConstantAmountTotal;
            licenceInfo.licences[30].TempAmountTotal = simserviceTempAmountTotal;
            licenceInfo.licences[30].HistoryBtn = simserviceHistoryBtn;

            licenceInfo.licences[31].ConstantAmountTextbox = barcodesadminConstantAmountTextbox;
            licenceInfo.licences[31].TempAmountTextbox = barcodesadminTempAmountTextbox;
            licenceInfo.licences[31].TempDatebox = barcodesadminTempDatebox;
            licenceInfo.licences[31].ConstantHelpdeskEndDatebox = barcodesadminConstantHelpdeskEndDatebox;
            licenceInfo.licences[31].ConstantGuaranteeEndDatebox = barcodesadminConstantGuaranteeEndDatebox;
            licenceInfo.licences[31].TempHelpdeskEndDatebox = barcodesadminTempHelpdeskEndDatebox;
            licenceInfo.licences[31].TempGuaranteeEndDatebox = barcodesadminTempGuaranteeEndDatebox;
            licenceInfo.licences[31].ConstantAmountTotal = barcodesadminConstantAmountTotal;
            licenceInfo.licences[31].TempAmountTotal = barcodesadminTempAmountTotal;
            licenceInfo.licences[31].HistoryBtn = barcodesadminHistoryBtn;

            licenceInfo.licences[32].ConstantAmountTextbox = gatesmartsConstantAmountTextbox;
            licenceInfo.licences[32].TempAmountTextbox = gatesmartsTempAmountTextbox;
            licenceInfo.licences[32].TempDatebox = gatesmartsTempDatebox;

            licenceInfo.licences[33].ConstantAmountTextbox = simserviceConstantAmountTextbox;
            licenceInfo.licences[33].TempAmountTextbox = simserviceTempAmountTextbox;
            licenceInfo.licences[33].TempDatebox = simserviceTempDatebox;
            licenceInfo.licences[33].ConstantHelpdeskEndDatebox = simserviceConstantHelpdeskEndDatebox;
            licenceInfo.licences[33].ConstantGuaranteeEndDatebox = simserviceConstantGuaranteeEndDatebox;
            licenceInfo.licences[33].TempHelpdeskEndDatebox = simserviceTempHelpdeskEndDatebox;
            licenceInfo.licences[33].TempGuaranteeEndDatebox = simserviceTempGuaranteeEndDatebox;
            licenceInfo.licences[33].ConstantAmountTotal = simserviceConstantAmountTotal;
            licenceInfo.licences[33].TempAmountTotal = simserviceTempAmountTotal;
            licenceInfo.licences[33].HistoryBtn = simserviceHistoryBtn;

            licenceInfo.licences[34].ConstantAmountTextbox = armnvdConstantAmountTextbox;
            licenceInfo.licences[34].TempAmountTextbox = armnvdTempAmountTextbox;
            licenceInfo.licences[34].TempDatebox = armnvdTempDatebox;
            licenceInfo.licences[34].ConstantHelpdeskEndDatebox = armnvdConstantHelpdeskEndDatebox;
            licenceInfo.licences[34].ConstantGuaranteeEndDatebox = armnvdConstantGuaranteeEndDatebox;
            licenceInfo.licences[34].TempHelpdeskEndDatebox = armnvdTempHelpdeskEndDatebox;
            licenceInfo.licences[34].TempGuaranteeEndDatebox = armnvdTempGuaranteeEndDatebox;
            licenceInfo.licences[34].ConstantAmountTotal = armnvdConstantAmountTotal;
            licenceInfo.licences[34].TempAmountTotal = armnvdTempAmountTotal;
            licenceInfo.licences[34].HistoryBtn = armnvdHistoryBtn;

            licenceInfo.licences[35].ConstantAmountTextbox = devicecontrolserverConstantAmountTextbox;
            licenceInfo.licences[35].TempAmountTextbox = devicecontrolserverTempAmountTextbox;
            licenceInfo.licences[35].TempDatebox = devicecontrolserverTempDatebox;
            licenceInfo.licences[35].ConstantHelpdeskEndDatebox = devicecontrolserverConstantHelpdeskEndDatebox;
            licenceInfo.licences[35].ConstantGuaranteeEndDatebox = devicecontrolserverConstantGuaranteeEndDatebox;
            licenceInfo.licences[35].TempHelpdeskEndDatebox = devicecontrolserverTempHelpdeskEndDatebox;
            licenceInfo.licences[35].TempGuaranteeEndDatebox = devicecontrolserverTempGuaranteeEndDatebox;
            licenceInfo.licences[35].ConstantAmountTotal = devicecontrolserverConstantAmountTotal;
            licenceInfo.licences[35].TempAmountTotal = devicecontrolserverTempAmountTotal;
            licenceInfo.licences[35].HistoryBtn = devicecontrolserverHistoryBtn;

            licenceInfo.licences[36].ConstantAmountTextbox = trassatestConstantAmountTextbox;
            licenceInfo.licences[36].TempAmountTextbox = trassatestTempAmountTextbox;
            licenceInfo.licences[36].TempDatebox = trassatestTempDatebox;

            licenceInfo.licences[37].ConstantAmountTextbox = trassavkpruConstantAmountTextbox;
            licenceInfo.licences[37].TempAmountTextbox = trassavkpruTempAmountTextbox;
            licenceInfo.licences[37].TempDatebox = trassavkpruTempDatebox;

            licenceInfo.licences[38].ConstantAmountTextbox = trassavkputcpipConstantAmountTextbox;
            licenceInfo.licences[38].TempAmountTextbox = trassavkputcpipTempAmountTextbox;
            licenceInfo.licences[38].TempDatebox = trassavkputcpipTempDatebox;

            licenceInfo.licences[39].ConstantAmountTextbox = trassavkpputcpipConstantAmountTextbox;
            licenceInfo.licences[39].TempAmountTextbox = trassavkpputcpipTempAmountTextbox;
            licenceInfo.licences[39].TempDatebox = trassavkpputcpipTempDatebox;

            licenceInfo.licences[40].ConstantAmountTextbox = gateservicevkConstantAmountTextbox;
            licenceInfo.licences[40].TempAmountTextbox = gateservicevkTempAmountTextbox;
            licenceInfo.licences[40].TempDatebox = gateservicevkTempDatebox;

            licenceInfo.licences[41].ConstantAmountTextbox = avk4ConstantAmountTextbox;
            licenceInfo.licences[41].TempAmountTextbox = avk4TempAmountTextbox;
            licenceInfo.licences[41].TempDatebox = avk4TempDatebox;

            licenceInfo.licences[42].ConstantAmountTextbox = magbiyaconnectionConstantAmountTextbox;
            licenceInfo.licences[42].TempAmountTextbox = magbiyaconnectionTempAmountTextbox;
            licenceInfo.licences[42].TempDatebox = magbiyaconnectionTempDatebox;
            licenceInfo.licences[42].ConstantHelpdeskEndDatebox = magbiyaconnectionConstantHelpdeskEndDatebox;
            licenceInfo.licences[42].ConstantGuaranteeEndDatebox = magbiyaconnectionConstantGuaranteeEndDatebox;
            licenceInfo.licences[42].TempHelpdeskEndDatebox = magbiyaconnectionTempHelpdeskEndDatebox;
            licenceInfo.licences[42].TempGuaranteeEndDatebox = magbiyaconnectionTempGuaranteeEndDatebox;
            licenceInfo.licences[42].ConstantAmountTotal = magbiyaconnectionConstantAmountTotal;
            licenceInfo.licences[42].TempAmountTotal = magbiyaconnectionTempAmountTotal;
            licenceInfo.licences[42].HistoryBtn = magbiyaconnectionHistoryBtn;

            licenceInfo.licences[43].ConstantAmountTextbox = voiceidentificationConstantAmountTextbox;
            licenceInfo.licences[43].TempAmountTextbox = voiceidentificationTempAmountTextbox;
            licenceInfo.licences[43].TempDatebox = voiceidentificationTempDatebox;
            licenceInfo.licences[43].ConstantHelpdeskEndDatebox = voiceidentificationConstantHelpdeskEndDatebox;
            licenceInfo.licences[43].ConstantGuaranteeEndDatebox = voiceidentificationConstantGuaranteeEndDatebox;
            licenceInfo.licences[43].TempHelpdeskEndDatebox = voiceidentificationTempHelpdeskEndDatebox;
            licenceInfo.licences[43].TempGuaranteeEndDatebox = voiceidentificationTempGuaranteeEndDatebox;
            licenceInfo.licences[43].ConstantAmountTotal = voiceidentificationConstantAmountTotal;
            licenceInfo.licences[43].TempAmountTotal = voiceidentificationTempAmountTotal;
            licenceInfo.licences[43].HistoryBtn = voiceidentificationHistoryBtn;

            licenceInfo.licences[44].ConstantAmountTextbox = mobileobjectstrackingConstantAmountTextbox;
            licenceInfo.licences[44].TempAmountTextbox = mobileobjectstrackingTempAmountTextbox;
            licenceInfo.licences[44].TempDatebox = mobileobjectstrackingTempDatebox;
            licenceInfo.licences[44].ConstantHelpdeskEndDatebox = mobileobjectstrackingConstantHelpdeskEndDatebox;
            licenceInfo.licences[44].ConstantGuaranteeEndDatebox = mobileobjectstrackingConstantGuaranteeEndDatebox;
            licenceInfo.licences[44].TempHelpdeskEndDatebox = mobileobjectstrackingTempHelpdeskEndDatebox;
            licenceInfo.licences[44].TempGuaranteeEndDatebox = mobileobjectstrackingTempGuaranteeEndDatebox;
            licenceInfo.licences[44].ConstantAmountTotal = mobileobjectstrackingConstantAmountTotal;
            licenceInfo.licences[44].TempAmountTotal = mobileobjectstrackingTempAmountTotal;
            licenceInfo.licences[44].HistoryBtn = mobileobjectstrackingHistoryBtn;

            licenceInfo.licences[45].ConstantAmountTextbox = barcodek2pConstantAmountTextbox;
            licenceInfo.licences[45].TempAmountTextbox = barcodek2pTempAmountTextbox;
            licenceInfo.licences[45].TempDatebox = barcodek2pTempDatebox;
            licenceInfo.licences[45].ConstantHelpdeskEndDatebox = barcodek2pConstantHelpdeskEndDatebox;
            licenceInfo.licences[45].ConstantGuaranteeEndDatebox = barcodek2pConstantGuaranteeEndDatebox;
            licenceInfo.licences[45].TempHelpdeskEndDatebox = barcodek2pTempHelpdeskEndDatebox;
            licenceInfo.licences[45].TempGuaranteeEndDatebox = barcodek2pTempGuaranteeEndDatebox;
            licenceInfo.licences[45].ConstantAmountTotal = barcodek2pConstantAmountTotal;
            licenceInfo.licences[45].TempAmountTotal = barcodek2pTempAmountTotal;
            licenceInfo.licences[45].HistoryBtn = barcodek2pHistoryBtn;

            licenceInfo.licences[46].ConstantAmountTextbox = barcodek2rConstantAmountTextbox;
            licenceInfo.licences[46].TempAmountTextbox = barcodek2rTempAmountTextbox;
            licenceInfo.licences[46].TempDatebox = barcodek2rTempDatebox;
            licenceInfo.licences[46].ConstantHelpdeskEndDatebox = barcodek2rConstantHelpdeskEndDatebox;
            licenceInfo.licences[46].ConstantGuaranteeEndDatebox = barcodek2rConstantGuaranteeEndDatebox;
            licenceInfo.licences[46].TempHelpdeskEndDatebox = barcodek2rTempHelpdeskEndDatebox;
            licenceInfo.licences[46].TempGuaranteeEndDatebox = barcodek2rTempGuaranteeEndDatebox;
            licenceInfo.licences[46].ConstantAmountTotal = barcodek2rConstantAmountTotal;
            licenceInfo.licences[46].TempAmountTotal = barcodek2rTempAmountTotal;
            licenceInfo.licences[46].HistoryBtn = barcodek2rHistoryBtn;

            licenceInfo.licences[47].ConstantAmountTextbox = barcodek2fConstantAmountTextbox;
            licenceInfo.licences[47].TempAmountTextbox = barcodek2fTempAmountTextbox;
            licenceInfo.licences[47].TempDatebox = barcodek2fTempDatebox;
            licenceInfo.licences[47].ConstantHelpdeskEndDatebox = barcodek2fConstantHelpdeskEndDatebox;
            licenceInfo.licences[47].ConstantGuaranteeEndDatebox = barcodek2fConstantGuaranteeEndDatebox;
            licenceInfo.licences[47].TempHelpdeskEndDatebox = barcodek2fTempHelpdeskEndDatebox;
            licenceInfo.licences[47].TempGuaranteeEndDatebox = barcodek2fTempGuaranteeEndDatebox;
            licenceInfo.licences[47].ConstantAmountTotal = barcodek2fConstantAmountTotal;
            licenceInfo.licences[47].TempAmountTotal = barcodek2fTempAmountTotal;
            licenceInfo.licences[47].HistoryBtn = barcodek2fHistoryBtn;

            licenceInfo.licences[48].ConstantAmountTextbox = accountingbarcodeprintingConstantAmountTextbox;
            licenceInfo.licences[48].TempAmountTextbox = accountingbarcodeprintingTempAmountTextbox;
            licenceInfo.licences[48].TempDatebox = accountingbarcodeprintingTempDatebox;
            licenceInfo.licences[48].ConstantHelpdeskEndDatebox = accountingbarcodeprintingConstantHelpdeskEndDatebox;
            licenceInfo.licences[48].ConstantGuaranteeEndDatebox = accountingbarcodeprintingConstantGuaranteeEndDatebox;
            licenceInfo.licences[48].TempHelpdeskEndDatebox = accountingbarcodeprintingTempHelpdeskEndDatebox;
            licenceInfo.licences[48].TempGuaranteeEndDatebox = accountingbarcodeprintingTempGuaranteeEndDatebox;
            licenceInfo.licences[48].ConstantAmountTotal = accountingbarcodeprintingConstantAmountTotal;
            licenceInfo.licences[48].TempAmountTotal = accountingbarcodeprintingTempAmountTotal;
            licenceInfo.licences[48].HistoryBtn = accountingbarcodeprintingHistoryBtn;

            licenceInfo.licences[49].ConstantAmountTextbox = analyzestatisticserviceConstantAmountTextbox;
            licenceInfo.licences[49].TempAmountTextbox = analyzestatisticserviceTempAmountTextbox;
            licenceInfo.licences[49].TempDatebox = analyzestatisticserviceTempDatebox;
            licenceInfo.licences[49].ConstantHelpdeskEndDatebox = analyzestatisticserviceConstantHelpdeskEndDatebox;
            licenceInfo.licences[49].ConstantGuaranteeEndDatebox = analyzestatisticserviceConstantGuaranteeEndDatebox;
            licenceInfo.licences[49].TempHelpdeskEndDatebox = analyzestatisticserviceTempHelpdeskEndDatebox;
            licenceInfo.licences[49].TempGuaranteeEndDatebox = analyzestatisticserviceTempGuaranteeEndDatebox;
            licenceInfo.licences[49].ConstantAmountTotal = analyzestatisticserviceConstantAmountTotal;
            licenceInfo.licences[49].TempAmountTotal = analyzestatisticserviceTempAmountTotal;
            licenceInfo.licences[49].HistoryBtn = analyzestatisticserviceHistoryBtn;

            licenceInfo.licences[50].ConstantAmountTextbox = avk2ConstantAmountTextbox;
            licenceInfo.licences[50].TempAmountTextbox = avk2TempAmountTextbox;
            licenceInfo.licences[50].TempDatebox = avk2TempDatebox;

            licenceInfo.licences[51].ConstantAmountTextbox = katunclientConstantAmountTextbox;
            licenceInfo.licences[51].TempAmountTextbox = katunclientTempAmountTextbox;
            licenceInfo.licences[51].TempDatebox = katunclientTempDatebox;

            licenceInfo.licences[52].ConstantAmountTextbox = gatenssConstantAmountTextbox;
            licenceInfo.licences[52].TempAmountTextbox = gatenssTempAmountTextbox;
            licenceInfo.licences[52].TempDatebox = gatenssTempDatebox;

            licenceInfo.licences[53].ConstantAmountTextbox = avk2uConstantAmountTextbox;
            licenceInfo.licences[53].TempAmountTextbox = avk2uTempAmountTextbox;
            licenceInfo.licences[53].TempDatebox = avk2uTempDatebox;

            licenceInfo.licences[54].ConstantAmountTextbox = gateistokConstantAmountTextbox;
            licenceInfo.licences[54].TempAmountTextbox = gateistokTempAmountTextbox;
            licenceInfo.licences[54].TempDatebox = gateistokTempDatebox;

            licenceInfo.licences[55].ConstantAmountTextbox = polyglotwebaccessConstantAmountTextbox;
            licenceInfo.licences[55].TempAmountTextbox = polyglotwebaccessTempAmountTextbox;
            licenceInfo.licences[55].TempDatebox = polyglotwebaccessTempDatebox;
            licenceInfo.licences[55].ConstantHelpdeskEndDatebox = polyglotwebaccessConstantHelpdeskEndDatebox;
            licenceInfo.licences[55].ConstantGuaranteeEndDatebox = polyglotwebaccessConstantGuaranteeEndDatebox;
            licenceInfo.licences[55].TempHelpdeskEndDatebox = polyglotwebaccessTempHelpdeskEndDatebox;
            licenceInfo.licences[55].TempGuaranteeEndDatebox = polyglotwebaccessTempGuaranteeEndDatebox;
            licenceInfo.licences[55].ConstantAmountTotal = polyglotwebaccessConstantAmountTotal;
            licenceInfo.licences[55].TempAmountTotal = polyglotwebaccessTempAmountTotal;
            licenceInfo.licences[55].HistoryBtn = polyglotwebaccessHistoryBtn;

            licenceInfo.licences[56].ConstantAmountTextbox = magservercomponentsConstantAmountTextbox;
            licenceInfo.licences[56].TempAmountTextbox = magservercomponentsTempAmountTextbox;
            licenceInfo.licences[56].TempDatebox = magservercomponentsTempDatebox;
            licenceInfo.licences[56].ConstantHelpdeskEndDatebox = magservercomponentsConstantHelpdeskEndDatebox;
            licenceInfo.licences[56].ConstantGuaranteeEndDatebox = magservercomponentsConstantGuaranteeEndDatebox;
            licenceInfo.licences[56].TempHelpdeskEndDatebox = magservercomponentsTempHelpdeskEndDatebox;
            licenceInfo.licences[56].TempGuaranteeEndDatebox = magservercomponentsTempGuaranteeEndDatebox;
            licenceInfo.licences[56].ConstantAmountTotal = magservercomponentsConstantAmountTotal;
            licenceInfo.licences[56].TempAmountTotal = magservercomponentsTempAmountTotal;
            licenceInfo.licences[56].HistoryBtn = magservercomponentsHistoryBtn;

            licenceInfo.licences[57].ConstantAmountTextbox = rampaaudioConstantAmountTextbox;
            licenceInfo.licences[57].TempAmountTextbox = rampaaudioTempAmountTextbox;
            licenceInfo.licences[57].TempDatebox = rampaaudioTempDatebox;

            licenceInfo.licences[58].ConstantAmountTextbox = gatetele2ConstantAmountTextbox;
            licenceInfo.licences[58].TempAmountTextbox = gatetele2TempAmountTextbox;
            licenceInfo.licences[58].TempDatebox = gatetele2TempDatebox;

            licenceInfo.licences[59].ConstantAmountTextbox = onvifdeviceConstantAmountTextbox;
            licenceInfo.licences[59].TempAmountTextbox = onvifdeviceTempAmountTextbox;
            licenceInfo.licences[59].TempDatebox = onvifdeviceTempDatebox;

            licenceInfo.licences[60].ConstantAmountTextbox = oracleserverbaseConstantAmountTextbox;
            licenceInfo.licences[60].TempAmountTextbox = oracleserverbaseTempAmountTextbox;
            licenceInfo.licences[60].TempDatebox = oracleserverbaseTempDatebox;
            licenceInfo.licences[60].ConstantHelpdeskEndDatebox = oracleserverbaseConstantHelpdeskEndDatebox;
            licenceInfo.licences[60].ConstantGuaranteeEndDatebox = oracleserverbaseConstantGuaranteeEndDatebox;
            licenceInfo.licences[60].TempHelpdeskEndDatebox = oracleserverbaseTempHelpdeskEndDatebox;
            licenceInfo.licences[60].TempGuaranteeEndDatebox = oracleserverbaseTempGuaranteeEndDatebox;
            licenceInfo.licences[60].ConstantAmountTotal = oracleserverbaseConstantAmountTotal;
            licenceInfo.licences[60].TempAmountTotal = oracleserverbaseTempAmountTotal;
            licenceInfo.licences[60].HistoryBtn = oracleserverbaseHistoryBtn;

            licenceInfo.licences[61].ConstantAmountTextbox = oracleworkstationbaseConstantAmountTextbox;
            licenceInfo.licences[61].TempAmountTextbox = oracleworkstationbaseTempAmountTextbox;
            licenceInfo.licences[61].TempDatebox = oracleworkstationbaseTempDatebox;
            licenceInfo.licences[61].ConstantHelpdeskEndDatebox = oracleworkstationbaseConstantHelpdeskEndDatebox;
            licenceInfo.licences[61].ConstantGuaranteeEndDatebox = oracleworkstationbaseConstantGuaranteeEndDatebox;
            licenceInfo.licences[61].TempHelpdeskEndDatebox = oracleworkstationbaseTempHelpdeskEndDatebox;
            licenceInfo.licences[61].TempGuaranteeEndDatebox = oracleworkstationbaseTempGuaranteeEndDatebox;
            licenceInfo.licences[61].ConstantAmountTotal = oracleworkstationbaseConstantAmountTotal;
            licenceInfo.licences[61].TempAmountTotal = oracleworkstationbaseTempAmountTotal;
            licenceInfo.licences[61].HistoryBtn = oracleworkstationbaseHistoryBtn;

            licenceInfo.licences[62].ConstantAmountTextbox = hatamagConstantAmountTextbox;
            licenceInfo.licences[62].TempAmountTextbox = hatamagTempAmountTextbox;
            licenceInfo.licences[62].TempDatebox = hatamagTempDatebox;

            licenceInfo.licences[63].ConstantAmountTextbox = billclientg2ConstantAmountTextbox;
            licenceInfo.licences[63].TempAmountTextbox = billclientg2TempAmountTextbox;
            licenceInfo.licences[63].TempDatebox = billclientg2TempDatebox;
            licenceInfo.licences[63].ConstantHelpdeskEndDatebox = billclientg2ConstantHelpdeskEndDatebox;
            licenceInfo.licences[63].ConstantGuaranteeEndDatebox = billclientg2ConstantGuaranteeEndDatebox;
            licenceInfo.licences[63].TempHelpdeskEndDatebox = billclientg2TempHelpdeskEndDatebox;
            licenceInfo.licences[63].TempGuaranteeEndDatebox = billclientg2TempGuaranteeEndDatebox;
            licenceInfo.licences[63].ConstantAmountTotal = billclientg2ConstantAmountTotal;
            licenceInfo.licences[63].TempAmountTotal = billclientg2TempAmountTotal;
            licenceInfo.licences[63].HistoryBtn = billclientg2HistoryBtn;

            licenceInfo.licences[64].ConstantAmountTextbox = gateConstantAmountTextbox;
            licenceInfo.licences[64].TempAmountTextbox = gateTempAmountTextbox;
            licenceInfo.licences[64].TempDatebox = gateTempDatebox;
            licenceInfo.licences[64].ConstantHelpdeskEndDatebox = gateConstantHelpdeskEndDatebox;
            licenceInfo.licences[64].ConstantGuaranteeEndDatebox = gateConstantGuaranteeEndDatebox;
            licenceInfo.licences[64].TempHelpdeskEndDatebox = gateTempHelpdeskEndDatebox;
            licenceInfo.licences[64].TempGuaranteeEndDatebox = gateTempGuaranteeEndDatebox;
            licenceInfo.licences[64].ConstantAmountTotal = gateConstantAmountTotal;
            licenceInfo.licences[64].TempAmountTotal = gateTempAmountTotal;
            licenceInfo.licences[64].HistoryBtn = gateHistoryBtn;

            licenceInfo.licences[65].ConstantAmountTextbox = kolpakConstantAmountTextbox;
            licenceInfo.licences[65].TempAmountTextbox = kolpakTempAmountTextbox;
            licenceInfo.licences[65].TempDatebox = kolpakTempDatebox;
            licenceInfo.licences[65].ConstantHelpdeskEndDatebox = kolpakConstantHelpdeskEndDatebox;
            licenceInfo.licences[65].ConstantGuaranteeEndDatebox = kolpakConstantGuaranteeEndDatebox;
            licenceInfo.licences[65].TempHelpdeskEndDatebox = kolpakTempHelpdeskEndDatebox;
            licenceInfo.licences[65].TempGuaranteeEndDatebox = kolpakTempGuaranteeEndDatebox;
            licenceInfo.licences[65].ConstantAmountTotal = kolpakConstantAmountTotal;
            licenceInfo.licences[65].TempAmountTotal = kolpakTempAmountTotal;
            licenceInfo.licences[65].HistoryBtn = kolpakHistoryBtn;

            licenceInfo.licences[66].ConstantAmountTextbox = playbackminingConstantAmountTextbox;
            licenceInfo.licences[66].TempAmountTextbox = playbackminingTempAmountTextbox;
            licenceInfo.licences[66].TempDatebox = playbackminingTempDatebox;
            licenceInfo.licences[66].ConstantHelpdeskEndDatebox = playbackminingConstantHelpdeskEndDatebox;
            licenceInfo.licences[66].ConstantGuaranteeEndDatebox = playbackminingConstantGuaranteeEndDatebox;
            licenceInfo.licences[66].TempHelpdeskEndDatebox = playbackminingTempHelpdeskEndDatebox;
            licenceInfo.licences[66].TempGuaranteeEndDatebox = playbackminingTempGuaranteeEndDatebox;
            licenceInfo.licences[66].ConstantAmountTotal = playbackminingConstantAmountTotal;
            licenceInfo.licences[66].TempAmountTotal = playbackminingTempAmountTotal;
            licenceInfo.licences[66].HistoryBtn = playbackminingHistoryBtn;

            //здесь добавление инфы о новых лицензиях
        }

        //установка значений Datebox'ов
        private void DateboxInit()
        {
            
            foreach (StelthXmlLicence licence in licenceInfo.licences)
            {
                licence.TempDatebox.Value = DateTime.Now;
                licence.TempDatebox.Value = licence.TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

                if (licence.Version == 2)
                {
                    licence.ConstantHelpdeskEndDatebox.Value = DateTime.Now;
                    licence.ConstantHelpdeskEndDatebox.Value = licence.ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
                    licence.ConstantGuaranteeEndDatebox.Value = DateTime.Now;
                    licence.ConstantGuaranteeEndDatebox.Value = licence.ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
                    licence.TempHelpdeskEndDatebox.Value = DateTime.Now;
                    licence.TempHelpdeskEndDatebox.Value = licence.TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
                    licence.TempGuaranteeEndDatebox.Value = DateTime.Now;
                    licence.TempGuaranteeEndDatebox.Value = licence.TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
                }
            }
 /*           
            //1
            magreadTempDatebox.Value = DateTime.Now;
            magreadTempDatebox.Value = magreadTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //2
            taskjournalTempDatebox.Value = DateTime.Now;
            taskjournalTempDatebox.Value = taskjournalTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //3
            magfaxTempDatebox.Value = DateTime.Now;
            magfaxTempDatebox.Value = magfaxTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //4
            trackingTempDatebox.Value = DateTime.Now;
            trackingTempDatebox.Value = trackingTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //5
            //audioplayerTempDatebox.Value = DateTime.Now;
            //audioplayerTempDatebox.Value = audioplayerTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //6
            billclientTempDatebox.Value = DateTime.Now;
            billclientTempDatebox.Value = billclientTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //7
            mwieserverTempDatebox.Value = DateTime.Now;
            mwieserverTempDatebox.Value = mwieserverTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //8
            mwieclientTempDatebox.Value = DateTime.Now;
            mwieclientTempDatebox.Value = mwieclientTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //9
            mwiestationTempDatebox.Value = DateTime.Now;
            mwiestationTempDatebox.Value = mwiestationTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //10
            integrationserverTempDatebox.Value = DateTime.Now;
            integrationserverTempDatebox.Value = integrationserverTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //11
            integrationadapterTempDatebox.Value = DateTime.Now;
            integrationadapterTempDatebox.Value = integrationadapterTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //12
            videojournalTempDatebox.Value = DateTime.Now;
            videojournalTempDatebox.Value = videojournalTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //13
            imitatorsormTempDatebox.Value = DateTime.Now;
            imitatorsormTempDatebox.Value = imitatorsormTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //14
            maggradientTempDatebox.Value = DateTime.Now;
            maggradientTempDatebox.Value = maggradientTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //15
            tracking3TempDatebox.Value = DateTime.Now;
            tracking3TempDatebox.Value = tracking3TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //16
            gatemegafonTempDatebox.Value = DateTime.Now;
            gatemegafonTempDatebox.Value = gatemegafonTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //17
            gatemtsTempDatebox.Value = DateTime.Now;
            gatemtsTempDatebox.Value = gatemtsTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //18
            gatebeelineTempDatebox.Value = DateTime.Now;
            gatebeelineTempDatebox.Value = gatebeelineTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //19
            istokclientTempDatebox.Value = DateTime.Now;
            istokclientTempDatebox.Value = istokclientTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //20
            higinaconverterTempDatebox.Value = DateTime.Now;
            higinaconverterTempDatebox.Value = higinaconverterTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //21
            webmapsTempDatebox.Value = DateTime.Now;
            webmapsTempDatebox.Value = webmapsTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //22
            gateyanvarTempDatebox.Value = DateTime.Now;
            gateyanvarTempDatebox.Value = gateyanvarTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //23
            play3gvideoTempDatebox.Value = DateTime.Now;
            play3gvideoTempDatebox.Value = play3gvideoTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //24
            integrationnetconfigTempDatebox.Value = DateTime.Now;
            integrationnetconfigTempDatebox.Value = integrationnetconfigTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //25
            trackingg2TempDatebox.Value = DateTime.Now;
            trackingg2TempDatebox.Value = trackingg2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            trackingg2ConstantHelpdeskEndDatebox.Value = DateTime.Now;
            trackingg2ConstantHelpdeskEndDatebox.Value = trackingg2ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            trackingg2ConstantGuaranteeEndDatebox.Value = DateTime.Now;
            trackingg2ConstantGuaranteeEndDatebox.Value = trackingg2ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            trackingg2TempHelpdeskEndDatebox.Value = DateTime.Now;
            trackingg2TempHelpdeskEndDatebox.Value = trackingg2TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            trackingg2TempGuaranteeEndDatebox.Value = DateTime.Now;
            trackingg2TempGuaranteeEndDatebox.Value = trackingg2TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //26
            webmapsg2TempDatebox.Value = DateTime.Now;
            webmapsg2TempDatebox.Value = webmapsg2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            webmapsg2ConstantHelpdeskEndDatebox.Value = DateTime.Now;
            webmapsg2ConstantHelpdeskEndDatebox.Value = webmapsg2ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            webmapsg2ConstantGuaranteeEndDatebox.Value = DateTime.Now;
            webmapsg2ConstantGuaranteeEndDatebox.Value = webmapsg2ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            webmapsg2TempHelpdeskEndDatebox.Value = DateTime.Now;
            webmapsg2TempHelpdeskEndDatebox.Value = webmapsg2TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            webmapsg2TempGuaranteeEndDatebox.Value = DateTime.Now;
            webmapsg2TempGuaranteeEndDatebox.Value = webmapsg2TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //27
            magreadg2TempDatebox.Value = DateTime.Now;
            magreadg2TempDatebox.Value = magreadg2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magreadg2ConstantHelpdeskEndDatebox.Value = DateTime.Now;
            magreadg2ConstantHelpdeskEndDatebox.Value = magreadg2ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            magreadg2ConstantGuaranteeEndDatebox.Value = DateTime.Now;
            magreadg2ConstantGuaranteeEndDatebox.Value = magreadg2ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            magreadg2TempHelpdeskEndDatebox.Value = DateTime.Now;
            magreadg2TempHelpdeskEndDatebox.Value = magreadg2TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magreadg2TempGuaranteeEndDatebox.Value = DateTime.Now;
            magreadg2TempGuaranteeEndDatebox.Value = magreadg2TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //28
            videojournalg2TempDatebox.Value = DateTime.Now;
            videojournalg2TempDatebox.Value = videojournalg2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            videojournalg2ConstantHelpdeskEndDatebox.Value = DateTime.Now;
            videojournalg2ConstantHelpdeskEndDatebox.Value = videojournalg2ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            videojournalg2ConstantGuaranteeEndDatebox.Value = DateTime.Now;
            videojournalg2ConstantGuaranteeEndDatebox.Value = videojournalg2ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            videojournalg2TempHelpdeskEndDatebox.Value = DateTime.Now;
            videojournalg2TempHelpdeskEndDatebox.Value = videojournalg2TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            videojournalg2TempGuaranteeEndDatebox.Value = DateTime.Now;
            videojournalg2TempGuaranteeEndDatebox.Value = videojournalg2TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //29
            gateutkTempDatebox.Value = DateTime.Now;
            gateutkTempDatebox.Value = gateutkTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //30
            smsserviceTempDatebox.Value = DateTime.Now;
            smsserviceTempDatebox.Value = smsserviceTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            smsserviceConstantHelpdeskEndDatebox.Value = DateTime.Now;
            smsserviceConstantHelpdeskEndDatebox.Value = smsserviceConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            smsserviceConstantGuaranteeEndDatebox.Value = DateTime.Now;
            smsserviceConstantGuaranteeEndDatebox.Value = smsserviceConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            smsserviceTempHelpdeskEndDatebox.Value = DateTime.Now;
            smsserviceTempHelpdeskEndDatebox.Value = smsserviceTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            smsserviceTempGuaranteeEndDatebox.Value = DateTime.Now;
            smsserviceTempGuaranteeEndDatebox.Value = smsserviceTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //31
            simserviceTempDatebox.Value = DateTime.Now;
            simserviceTempDatebox.Value = simserviceTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            simserviceConstantHelpdeskEndDatebox.Value = DateTime.Now;
            simserviceConstantHelpdeskEndDatebox.Value = simserviceConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            simserviceConstantGuaranteeEndDatebox.Value = DateTime.Now;
            simserviceConstantGuaranteeEndDatebox.Value = simserviceConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            simserviceTempHelpdeskEndDatebox.Value = DateTime.Now;
            simserviceTempHelpdeskEndDatebox.Value = simserviceTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            simserviceTempGuaranteeEndDatebox.Value = DateTime.Now;
            simserviceTempGuaranteeEndDatebox.Value = simserviceTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //32
            barcodesadminTempDatebox.Value = DateTime.Now;
            barcodesadminTempDatebox.Value = barcodesadminTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodesadminConstantHelpdeskEndDatebox.Value = DateTime.Now;
            barcodesadminConstantHelpdeskEndDatebox.Value = barcodesadminConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            barcodesadminConstantGuaranteeEndDatebox.Value = DateTime.Now;
            barcodesadminConstantGuaranteeEndDatebox.Value = barcodesadminConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            barcodesadminTempHelpdeskEndDatebox.Value = DateTime.Now;
            barcodesadminTempHelpdeskEndDatebox.Value = barcodesadminTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodesadminTempGuaranteeEndDatebox.Value = DateTime.Now;
            barcodesadminTempGuaranteeEndDatebox.Value = barcodesadminTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //33
            gatesmartsTempDatebox.Value = DateTime.Now;
            gatesmartsTempDatebox.Value = gatesmartsTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //34
            simmagplaceserviceTempDatebox.Value = DateTime.Now;
            simmagplaceserviceTempDatebox.Value = simmagplaceserviceTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            simmagplaceserviceConstantHelpdeskEndDatebox.Value = DateTime.Now;
            simmagplaceserviceConstantHelpdeskEndDatebox.Value = simmagplaceserviceConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            simmagplaceserviceConstantGuaranteeEndDatebox.Value = DateTime.Now;
            simmagplaceserviceConstantGuaranteeEndDatebox.Value = simmagplaceserviceConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            simmagplaceserviceTempHelpdeskEndDatebox.Value = DateTime.Now;
            simmagplaceserviceTempHelpdeskEndDatebox.Value = simmagplaceserviceTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            simmagplaceserviceTempGuaranteeEndDatebox.Value = DateTime.Now;
            simmagplaceserviceTempGuaranteeEndDatebox.Value = simmagplaceserviceTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //35
            armnvdTempDatebox.Value = DateTime.Now;
            armnvdTempDatebox.Value = armnvdTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            armnvdConstantHelpdeskEndDatebox.Value = DateTime.Now;
            armnvdConstantHelpdeskEndDatebox.Value = armnvdConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            armnvdConstantGuaranteeEndDatebox.Value = DateTime.Now;
            armnvdConstantGuaranteeEndDatebox.Value = armnvdConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            armnvdTempHelpdeskEndDatebox.Value = DateTime.Now;
            armnvdTempHelpdeskEndDatebox.Value = armnvdTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            armnvdTempGuaranteeEndDatebox.Value = DateTime.Now;
            armnvdTempGuaranteeEndDatebox.Value = armnvdTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //36
            devicecontrolserverTempDatebox.Value = DateTime.Now;
            devicecontrolserverTempDatebox.Value = devicecontrolserverTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            devicecontrolserverConstantHelpdeskEndDatebox.Value = DateTime.Now;
            devicecontrolserverConstantHelpdeskEndDatebox.Value = devicecontrolserverConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            devicecontrolserverConstantGuaranteeEndDatebox.Value = DateTime.Now;
            devicecontrolserverConstantGuaranteeEndDatebox.Value = devicecontrolserverConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            devicecontrolserverTempHelpdeskEndDatebox.Value = DateTime.Now;
            devicecontrolserverTempHelpdeskEndDatebox.Value = devicecontrolserverTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            devicecontrolserverTempGuaranteeEndDatebox.Value = DateTime.Now;
            devicecontrolserverTempGuaranteeEndDatebox.Value = devicecontrolserverTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //37
            trassatestTempDatebox.Value = DateTime.Now;
            trassatestTempDatebox.Value = trassatestTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //38
            trassavkpruTempDatebox.Value = DateTime.Now;
            trassavkpruTempDatebox.Value = trassavkpruTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //39
            trassavkputcpipTempDatebox.Value = DateTime.Now;
            trassavkputcpipTempDatebox.Value = trassavkputcpipTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //40
            trassavkpputcpipTempDatebox.Value = DateTime.Now;
            trassavkpputcpipTempDatebox.Value = trassavkpputcpipTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //41
            gateservicevkTempDatebox.Value = DateTime.Now;
            gateservicevkTempDatebox.Value = gateservicevkTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //42
            avk4TempDatebox.Value = DateTime.Now;
            avk4TempDatebox.Value = avk4TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //43
            magbiyaconnectionTempDatebox.Value = DateTime.Now;
            magbiyaconnectionTempDatebox.Value = magbiyaconnectionTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            magbiyaconnectionTempDatebox.Value = DateTime.Now;
            magbiyaconnectionTempDatebox.Value = magbiyaconnectionTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magbiyaconnectionConstantHelpdeskEndDatebox.Value = DateTime.Now;
            magbiyaconnectionConstantHelpdeskEndDatebox.Value = magbiyaconnectionConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            magbiyaconnectionConstantGuaranteeEndDatebox.Value = DateTime.Now;
            magbiyaconnectionConstantGuaranteeEndDatebox.Value = magbiyaconnectionConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            magbiyaconnectionTempHelpdeskEndDatebox.Value = DateTime.Now;
            magbiyaconnectionTempHelpdeskEndDatebox.Value = magbiyaconnectionTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magbiyaconnectionTempGuaranteeEndDatebox.Value = DateTime.Now;
            magbiyaconnectionTempGuaranteeEndDatebox.Value = magbiyaconnectionTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //44
            voiceidentificationTempDatebox.Value = DateTime.Now;
            voiceidentificationTempDatebox.Value = voiceidentificationTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            voiceidentificationTempDatebox.Value = DateTime.Now;
            voiceidentificationTempDatebox.Value = voiceidentificationTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            voiceidentificationConstantHelpdeskEndDatebox.Value = DateTime.Now;
            voiceidentificationConstantHelpdeskEndDatebox.Value = voiceidentificationConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            voiceidentificationConstantGuaranteeEndDatebox.Value = DateTime.Now;
            voiceidentificationConstantGuaranteeEndDatebox.Value = voiceidentificationConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            voiceidentificationTempHelpdeskEndDatebox.Value = DateTime.Now;
            voiceidentificationTempHelpdeskEndDatebox.Value = voiceidentificationTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            voiceidentificationTempGuaranteeEndDatebox.Value = DateTime.Now;
            voiceidentificationTempGuaranteeEndDatebox.Value = voiceidentificationTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //45
            mobileobjectstrackingTempDatebox.Value = DateTime.Now;
            mobileobjectstrackingTempDatebox.Value = mobileobjectstrackingTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            mobileobjectstrackingConstantHelpdeskEndDatebox.Value = DateTime.Now;
            mobileobjectstrackingConstantHelpdeskEndDatebox.Value = mobileobjectstrackingConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            mobileobjectstrackingConstantGuaranteeEndDatebox.Value = DateTime.Now;
            mobileobjectstrackingConstantGuaranteeEndDatebox.Value = mobileobjectstrackingConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            mobileobjectstrackingTempHelpdeskEndDatebox.Value = DateTime.Now;
            mobileobjectstrackingTempHelpdeskEndDatebox.Value = mobileobjectstrackingTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            mobileobjectstrackingTempGuaranteeEndDatebox.Value = DateTime.Now;
            mobileobjectstrackingTempGuaranteeEndDatebox.Value = mobileobjectstrackingTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //46
            barcodek2pTempDatebox.Value = DateTime.Now;
            barcodek2pTempDatebox.Value = barcodek2pTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2pConstantHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2pConstantHelpdeskEndDatebox.Value = barcodek2pConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            barcodek2pConstantGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2pConstantGuaranteeEndDatebox.Value = barcodek2pConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            barcodek2pTempHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2pTempHelpdeskEndDatebox.Value = barcodek2pTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2pTempGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2pTempGuaranteeEndDatebox.Value = barcodek2pTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //47
            barcodek2rTempDatebox.Value = DateTime.Now;
            barcodek2rTempDatebox.Value = barcodek2rTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2rConstantHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2rConstantHelpdeskEndDatebox.Value = barcodek2rConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            barcodek2rConstantGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2rConstantGuaranteeEndDatebox.Value = barcodek2rConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            barcodek2rTempHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2rTempHelpdeskEndDatebox.Value = barcodek2rTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2rTempGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2rTempGuaranteeEndDatebox.Value = barcodek2rTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //48
            barcodek2fTempDatebox.Value = DateTime.Now;
            barcodek2fTempDatebox.Value = barcodek2fTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2fConstantHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2fConstantHelpdeskEndDatebox.Value =barcodek2fConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            barcodek2fConstantGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2fConstantGuaranteeEndDatebox.Value = barcodek2fConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            barcodek2fTempHelpdeskEndDatebox.Value = DateTime.Now;
            barcodek2fTempHelpdeskEndDatebox.Value = barcodek2fTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            barcodek2fTempGuaranteeEndDatebox.Value = DateTime.Now;
            barcodek2fTempGuaranteeEndDatebox.Value = barcodek2fTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //49
            accountingbarcodeprintingTempDatebox.Value = DateTime.Now;
            accountingbarcodeprintingTempDatebox.Value = accountingbarcodeprintingTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            accountingbarcodeprintingConstantHelpdeskEndDatebox.Value = DateTime.Now;
            accountingbarcodeprintingConstantHelpdeskEndDatebox.Value = accountingbarcodeprintingConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            accountingbarcodeprintingConstantGuaranteeEndDatebox.Value = DateTime.Now;
            accountingbarcodeprintingConstantGuaranteeEndDatebox.Value = accountingbarcodeprintingConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            accountingbarcodeprintingTempHelpdeskEndDatebox.Value = DateTime.Now;
            accountingbarcodeprintingTempHelpdeskEndDatebox.Value = accountingbarcodeprintingTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            accountingbarcodeprintingTempGuaranteeEndDatebox.Value = DateTime.Now;
            accountingbarcodeprintingTempGuaranteeEndDatebox.Value = accountingbarcodeprintingTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //50
            analyzestatisticserviceTempDatebox.Value = DateTime.Now;
            analyzestatisticserviceTempDatebox.Value = analyzestatisticserviceTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            analyzestatisticserviceConstantHelpdeskEndDatebox.Value = DateTime.Now;
            analyzestatisticserviceConstantHelpdeskEndDatebox.Value = analyzestatisticserviceConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            analyzestatisticserviceConstantGuaranteeEndDatebox.Value = DateTime.Now;
            analyzestatisticserviceConstantGuaranteeEndDatebox.Value = analyzestatisticserviceConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            analyzestatisticserviceTempHelpdeskEndDatebox.Value = DateTime.Now;
            analyzestatisticserviceTempHelpdeskEndDatebox.Value = analyzestatisticserviceTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            analyzestatisticserviceTempGuaranteeEndDatebox.Value = DateTime.Now;
            analyzestatisticserviceTempGuaranteeEndDatebox.Value = analyzestatisticserviceTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //51
            avk2TempDatebox.Value = DateTime.Now;
            avk2TempDatebox.Value = avk2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME); 

            //52
            katunclientTempDatebox.Value = DateTime.Now;
            katunclientTempDatebox.Value = katunclientTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME); 

            //53
            gatenssTempDatebox.Value = DateTime.Now;
            gatenssTempDatebox.Value = gatenssTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME); 

            //54
            avk2uTempDatebox.Value = DateTime.Now;
            avk2uTempDatebox.Value = avk2uTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //55
            gateistokTempDatebox.Value = DateTime.Now;
            gateistokTempDatebox.Value = gateistokTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //56
            polyglotwebaccessTempDatebox.Value = DateTime.Now;
            polyglotwebaccessTempDatebox.Value = polyglotwebaccessTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            polyglotwebaccessConstantHelpdeskEndDatebox.Value = DateTime.Now;
            polyglotwebaccessConstantHelpdeskEndDatebox.Value = polyglotwebaccessConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            polyglotwebaccessConstantGuaranteeEndDatebox.Value = DateTime.Now;
            polyglotwebaccessConstantGuaranteeEndDatebox.Value = polyglotwebaccessConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            polyglotwebaccessTempHelpdeskEndDatebox.Value = DateTime.Now;
            polyglotwebaccessTempHelpdeskEndDatebox.Value = polyglotwebaccessTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            polyglotwebaccessTempGuaranteeEndDatebox.Value = DateTime.Now;
            polyglotwebaccessTempGuaranteeEndDatebox.Value = polyglotwebaccessTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            
            //57
            magservercomponentsTempDatebox.Value = DateTime.Now;
            magservercomponentsTempDatebox.Value = magservercomponentsTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magservercomponentsConstantHelpdeskEndDatebox.Value = DateTime.Now;
            magservercomponentsConstantHelpdeskEndDatebox.Value = magservercomponentsConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            magservercomponentsConstantGuaranteeEndDatebox.Value = DateTime.Now;
            magservercomponentsConstantGuaranteeEndDatebox.Value = magservercomponentsConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            magservercomponentsTempHelpdeskEndDatebox.Value = DateTime.Now;
            magservercomponentsTempHelpdeskEndDatebox.Value = magservercomponentsTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            magservercomponentsTempGuaranteeEndDatebox.Value = DateTime.Now;
            magservercomponentsTempGuaranteeEndDatebox.Value = magservercomponentsTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //58
            rampaaudioTempDatebox.Value = DateTime.Now;
            rampaaudioTempDatebox.Value = rampaaudioTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //59
            gatetele2TempDatebox.Value = DateTime.Now;
            gatetele2TempDatebox.Value = gatetele2TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //60
            onvifdeviceTempDatebox.Value = DateTime.Now;
            onvifdeviceTempDatebox.Value = onvifdeviceTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //61
            oracleserverbaseTempDatebox.Value = DateTime.Now;
            oracleserverbaseTempDatebox.Value = oracleserverbaseTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            oracleserverbaseConstantHelpdeskEndDatebox.Value = DateTime.Now;
            oracleserverbaseConstantHelpdeskEndDatebox.Value = oracleserverbaseConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            oracleserverbaseConstantGuaranteeEndDatebox.Value = DateTime.Now;
            oracleserverbaseConstantGuaranteeEndDatebox.Value = oracleserverbaseConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            oracleserverbaseTempHelpdeskEndDatebox.Value = DateTime.Now;
            oracleserverbaseTempHelpdeskEndDatebox.Value = oracleserverbaseTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            oracleserverbaseTempGuaranteeEndDatebox.Value = DateTime.Now;
            oracleserverbaseTempGuaranteeEndDatebox.Value = oracleserverbaseTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);

            //62
            oracleworkstationbaseTempDatebox.Value = DateTime.Now;
            oracleworkstationbaseTempDatebox.Value = oracleworkstationbaseTempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            oracleworkstationbaseConstantHelpdeskEndDatebox.Value = DateTime.Now;
            oracleworkstationbaseConstantHelpdeskEndDatebox.Value = oracleworkstationbaseConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            oracleworkstationbaseConstantGuaranteeEndDatebox.Value = DateTime.Now;
            oracleworkstationbaseConstantGuaranteeEndDatebox.Value = oracleworkstationbaseConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            oracleworkstationbaseTempHelpdeskEndDatebox.Value = DateTime.Now;
            oracleworkstationbaseTempHelpdeskEndDatebox.Value = oracleworkstationbaseTempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            oracleworkstationbaseTempGuaranteeEndDatebox.Value = DateTime.Now;
            oracleworkstationbaseTempGuaranteeEndDatebox.Value = oracleworkstationbaseTempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
    */        
            //здесь добавление инфы о новых лицензиях
            /*
            TempDatebox.Value = DateTime.Now;
            TempDatebox.Value = TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME); 
             */
            /*
            TempDatebox.Value = DateTime.Now;
            TempDatebox.Value = TempDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            ConstantHelpdeskEndDatebox.Value = DateTime.Now;
            ConstantHelpdeskEndDatebox.Value = ConstantHelpdeskEndDatebox.Value.AddDays(StelthXml.HELPDESK_TIME);
            ConstantGuaranteeEndDatebox.Value = DateTime.Now;
            ConstantGuaranteeEndDatebox.Value = ConstantGuaranteeEndDatebox.Value.AddDays(StelthXml.GUARANTEE_TIME);
            TempHelpdeskEndDatebox.Value = DateTime.Now;
            TempHelpdeskEndDatebox.Value = TempHelpdeskEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            TempGuaranteeEndDatebox.Value = DateTime.Now;
            TempGuaranteeEndDatebox.Value = TempGuaranteeEndDatebox.Value.AddDays(StelthXml.TEMP_LICENCES_TIME);
            */
        }

        //очистка формы
        private void ClearForm()
        {
            newXml = null;

            //вкладка основной информации о прошивке
            dateBox.ResetText();
            cityTextbox.ResetText();
            departmentTextbox.ResetText();

            mainKeyCheckBox.Checked = true;
            reserveKeyCheckBox.Checked = true;

            mainKeyNumberTextbox.ResetText();
            mainKeyIdTextbox.ResetText();
            mainKeyCodeTextbox.ResetText();
            mainKeyRequestTextbox.ResetText();

            reserveKeyNumberTextbox.ResetText();
            reserveKeyIdTextbox.ResetText();
            reserveKeyCodeTextbox.ResetText();
            reserveKeyRequestTextbox.ResetText();

            //файл лицензий
            licenceInfo = new StelthXml();
            BindingFields(licenceInfo);

            //поля лицензий в форме
            foreach (StelthXmlLicence licence in licenceInfo.licences)
            {
                licence.ConstantAmountTextbox.ResetText();
                licence.TempAmountTextbox.ResetText();

                if (licence.Version == 2)
                {
                    licence.ConstantAmountTotal.ResetText();
                    licence.TempAmountTotal.ResetText();
                    licence.HistoryBtn.Enabled = false;
                }

            }

/*          //1
            magreadConstantAmountTextbox.ResetText();
            magreadTempAmountTextbox.ResetText();

            //2
            taskjournalConstantAmountTextbox.ResetText();
            taskjournalTempAmountTextbox.ResetText();

            //3
            magfaxConstantAmountTextbox.ResetText();
            magfaxTempAmountTextbox.ResetText();

            //4
            trackingConstantAmountTextbox.ResetText();
            trackingTempAmountTextbox.ResetText();

            //5
            audioplayerConstantAmountTextbox.ResetText();
            audioplayerTempAmountTextbox.ResetText();

            //6
            billclientConstantAmountTextbox.ResetText();
            billclientTempAmountTextbox.ResetText();

            //7
            mwieserverConstantAmountTextbox.ResetText();
            mwieserverTempAmountTextbox.ResetText();

            //8
            mwieclientConstantAmountTextbox.ResetText();
            mwieclientTempAmountTextbox.ResetText();

            //9
            mwiestationConstantAmountTextbox.ResetText();
            mwiestationTempAmountTextbox.ResetText();

            //10
            integrationserverConstantAmountTextbox.ResetText();
            integrationserverTempAmountTextbox.ResetText();

            //11
            integrationadapterConstantAmountTextbox.ResetText();
            integrationadapterTempAmountTextbox.ResetText();

            //12
            videojournalConstantAmountTextbox.ResetText();
            videojournalTempAmountTextbox.ResetText();

            //13
            imitatorsormConstantAmountTextbox.ResetText();
            imitatorsormTempAmountTextbox.ResetText();

            //14
            maggradientConstantAmountTextbox.ResetText();
            maggradientTempAmountTextbox.ResetText();

            //15
            tracking3ConstantAmountTextbox.ResetText();
            tracking3TempAmountTextbox.ResetText();

            //16
            gatemegafonConstantAmountTextbox.ResetText();
            gatemegafonTempAmountTextbox.ResetText();

            //17
            gatemtsConstantAmountTextbox.ResetText();
            gatemtsTempAmountTextbox.ResetText();

            //18
            gatebeelineConstantAmountTextbox.ResetText();
            gatebeelineTempAmountTextbox.ResetText();

            //19
            istokclientConstantAmountTextbox.ResetText();
            istokclientTempAmountTextbox.ResetText();

            //20
            higinaconverterConstantAmountTextbox.ResetText();
            higinaconverterTempAmountTextbox.ResetText();

            //21
            webmapsConstantAmountTextbox.ResetText();
            webmapsTempAmountTextbox.ResetText();

            //22
            gateyanvarConstantAmountTextbox.ResetText();
            gateyanvarTempAmountTextbox.ResetText();

            //23
            play3gvideoConstantAmountTextbox.ResetText();
            play3gvideoTempAmountTextbox.ResetText();

            //24
            integrationnetconfigConstantAmountTextbox.ResetText();
            integrationnetconfigTempAmountTextbox.ResetText();

            //25
            trackingg2ConstantAmountTextbox.ResetText();
            trackingg2TempAmountTextbox.ResetText();
            trackingg2ConstantAmountTotal.ResetText();
            trackingg2TempAmountTotal.ResetText();
            trackingg2HistoryBtn.Enabled = false;

            //26
            magreadg2ConstantAmountTextbox.ResetText();
            magreadg2TempAmountTextbox.ResetText();
            magreadg2ConstantAmountTotal.ResetText();
            magreadg2TempAmountTotal.ResetText();
            magreadg2HistoryBtn.Enabled = false;

            //27
            webmapsg2ConstantAmountTextbox.ResetText();
            webmapsg2TempAmountTextbox.ResetText();
            webmapsg2ConstantAmountTotal.ResetText();
            webmapsg2TempAmountTotal.ResetText();
            webmapsg2HistoryBtn.Enabled = false;

            //28
            videojournalg2ConstantAmountTextbox.ResetText();
            videojournalg2TempAmountTextbox.ResetText();
            videojournalg2ConstantAmountTotal.ResetText();
            videojournalg2TempAmountTotal.ResetText();
            videojournalg2HistoryBtn.Enabled = false;

            //29
            gateutkConstantAmountTextbox.ResetText();
            gateutkTempAmountTextbox.ResetText();

            //30
            smsserviceConstantAmountTextbox.ResetText();
            smsserviceTempAmountTextbox.ResetText();
            smsserviceConstantAmountTotal.ResetText();
            smsserviceTempAmountTotal.ResetText();
            smsserviceHistoryBtn.Enabled = false;

            //31
            simserviceConstantAmountTextbox.ResetText();
            simserviceTempAmountTextbox.ResetText();
            simserviceConstantAmountTotal.ResetText();
            simserviceTempAmountTotal.ResetText();
            simserviceHistoryBtn.Enabled = false;

            //32
            barcodesadminConstantAmountTextbox.ResetText();
            barcodesadminTempAmountTextbox.ResetText();
            barcodesadminConstantAmountTotal.ResetText();
            barcodesadminTempAmountTotal.ResetText();
            barcodesadminHistoryBtn.Enabled = false;

            //33
            gatesmartsConstantAmountTextbox.ResetText();
            gatesmartsTempAmountTextbox.ResetText();

            //34
            simmagplaceserviceConstantAmountTextbox.ResetText();
            simmagplaceserviceTempAmountTextbox.ResetText();
            simmagplaceserviceConstantAmountTotal.ResetText();
            simmagplaceserviceTempAmountTotal.ResetText();
            simmagplaceserviceHistoryBtn.Enabled = false;

            //35
            armnvdConstantAmountTextbox.ResetText();
            armnvdTempAmountTextbox.ResetText();
            armnvdConstantAmountTotal.ResetText();
            armnvdTempAmountTotal.ResetText();
            armnvdHistoryBtn.Enabled = false;

            //36
            devicecontrolserverConstantAmountTextbox.ResetText();
            devicecontrolserverTempAmountTextbox.ResetText();
            devicecontrolserverConstantAmountTotal.ResetText();
            devicecontrolserverTempAmountTotal.ResetText();
            devicecontrolserverHistoryBtn.Enabled = false;

            //37
            trassatestConstantAmountTextbox.ResetText();
            trassatestTempAmountTextbox.ResetText();

            //38
            trassavkpruConstantAmountTextbox.ResetText();
            trassavkpruTempAmountTextbox.ResetText();

            //39
            trassavkputcpipConstantAmountTextbox.ResetText();
            trassavkputcpipTempAmountTextbox.ResetText();

            //40
            trassavkpputcpipConstantAmountTextbox.ResetText();
            trassavkpputcpipTempAmountTextbox.ResetText();

            //41
            gateservicevkConstantAmountTextbox.ResetText();
            gateservicevkTempAmountTextbox.ResetText();

            //42
            avk4ConstantAmountTextbox.ResetText();
            avk4TempAmountTextbox.ResetText();

            //43
            magbiyaconnectionConstantAmountTextbox.ResetText();
            magbiyaconnectionTempAmountTextbox.ResetText();
            magbiyaconnectionConstantAmountTotal.ResetText();
            magbiyaconnectionTempAmountTotal.ResetText();
            magbiyaconnectionHistoryBtn.Enabled = false;

            //44
            voiceidentificationConstantAmountTextbox.ResetText();
            voiceidentificationTempAmountTextbox.ResetText();
            voiceidentificationConstantAmountTotal.ResetText();
            voiceidentificationTempAmountTotal.ResetText();
            voiceidentificationHistoryBtn.Enabled = false;

            //45
            mobileobjectstrackingConstantAmountTextbox.ResetText();
            mobileobjectstrackingTempAmountTextbox.ResetText();
            mobileobjectstrackingConstantAmountTotal.ResetText();
            mobileobjectstrackingTempAmountTotal.ResetText();
            mobileobjectstrackingHistoryBtn.Enabled = false;

            //46
            barcodek2pConstantAmountTextbox.ResetText();
            barcodek2pTempAmountTextbox.ResetText();
            barcodek2pConstantAmountTotal.ResetText();
            barcodek2pTempAmountTotal.ResetText();
            barcodek2pHistoryBtn.Enabled = false;

            //47
            barcodek2rConstantAmountTextbox.ResetText();
            barcodek2rTempAmountTextbox.ResetText();
            barcodek2rConstantAmountTotal.ResetText();
            barcodek2rTempAmountTotal.ResetText();
            barcodek2rHistoryBtn.Enabled = false;

            //48
            barcodek2fConstantAmountTextbox.ResetText();
            barcodek2fTempAmountTextbox.ResetText();
            barcodek2fConstantAmountTotal.ResetText();
            barcodek2fTempAmountTotal.ResetText();
            barcodek2fHistoryBtn.Enabled = false;

            //49
            accountingbarcodeprintingConstantAmountTextbox.ResetText();
            accountingbarcodeprintingTempAmountTextbox.ResetText();
            accountingbarcodeprintingConstantAmountTotal.ResetText();
            accountingbarcodeprintingTempAmountTotal.ResetText();
            accountingbarcodeprintingHistoryBtn.Enabled = false;

            //50
            analyzestatisticserviceConstantAmountTextbox.ResetText();
            analyzestatisticserviceTempAmountTextbox.ResetText();
            analyzestatisticserviceConstantAmountTotal.ResetText();
            analyzestatisticserviceTempAmountTotal.ResetText();
            analyzestatisticserviceHistoryBtn.Enabled = false;

            //51
            avk2ConstantAmountTextbox.ResetText();
            avk2TempAmountTextbox.ResetText();
            
            //52
            katunclientConstantAmountTextbox.ResetText();
            katunclientTempAmountTextbox.ResetText();

            //53
            gatenssConstantAmountTextbox.ResetText();
            gatenssTempAmountTextbox.ResetText();
            
            //54
            avk2uConstantAmountTextbox.ResetText();
            avk2uTempAmountTextbox.ResetText();
            
            //55
            gateistokConstantAmountTextbox.ResetText();
            gateistokTempAmountTextbox.ResetText();

            //56
            polyglotwebaccessConstantAmountTextbox.ResetText();
            polyglotwebaccessTempAmountTextbox.ResetText();
            polyglotwebaccessConstantAmountTotal.ResetText();
            polyglotwebaccessTempAmountTotal.ResetText();
            polyglotwebaccessHistoryBtn.Enabled = false;
            
            //57
            magservercomponentsConstantAmountTextbox.ResetText();
            magservercomponentsTempAmountTextbox.ResetText();
            magservercomponentsConstantAmountTotal.ResetText();
            magservercomponentsTempAmountTotal.ResetText();
            magservercomponentsHistoryBtn.Enabled = false;

            //58
            rampaaudioConstantAmountTextbox.ResetText();
            rampaaudioTempAmountTextbox.ResetText();

            //59
            gatetele2ConstantAmountTextbox.ResetText();
            gatetele2TempAmountTextbox.ResetText();

            //60
            onvifdeviceConstantAmountTextbox.ResetText();
            onvifdeviceTempAmountTextbox.ResetText();

            //61
            oracleserverbaseConstantAmountTextbox.ResetText();
            oracleserverbaseTempAmountTextbox.ResetText();
            oracleserverbaseConstantAmountTotal.ResetText();
            oracleserverbaseTempAmountTotal.ResetText();
            oracleserverbaseHistoryBtn.Enabled = false;

            //62
            oracleworkstationbaseConstantAmountTextbox.ResetText();
            oracleworkstationbaseTempAmountTextbox.ResetText();
            oracleworkstationbaseConstantAmountTotal.ResetText();
            oracleworkstationbaseTempAmountTotal.ResetText();
            oracleworkstationbaseHistoryBtn.Enabled = false;
*/
            //здесь добавление инфы о новых лицензиях
            /*
            ConstantAmountTextbox.ResetText();
            TempAmountTextbox.ResetText();
            */
            /*
            ConstantAmountTextbox.ResetText();
            TempAmountTextbox.ResetText();
            ConstantAmountTotal.ResetText();
            TempAmountTotal.ResetText();
            HistoryBtn.Enabled = false;
            */

            resultText = "";
            DateboxInit();
        }

        //кнопка "Новый XML"
        private void maintoolNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            mainForm.ActiveForm.Text = "Stelth III XML Filling";
            maintoolLicInfo.Enabled = false;
        }
        #endregion

        #region ОТКРЫТИЕ ФАЙЛА
        //кнопка "Открыть XML"
        private void maintoolOpen_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы XML (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            ClearForm();

            newXmlName = openFileDialog.FileName;
            newXml = XDocument.Load(newXmlName);
            //licenceInfo = new StelthXml();

            #region обработка раздела "license_pack"

            XElement firstLevel = newXml.Root.Element("license_pack");
            try
            {
                dateBox.Value = DateTime.Parse(firstLevel.Attribute("id").Value);
            }
            catch (FormatException)
            {
                dateBox.Value = DateTime.Now;
            }

            int n;
            int licenceConstantAmount = 0;
            int licenceTempAmount = 0;
            int compareResult = 0;
            DateTime licenceConstantHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceConstantGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceTempHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceTempGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);


            foreach (XElement secondLevel in firstLevel.Elements())
            {
                n = int.Parse(secondLevel.Attribute("product_id").Value);
                licenceConstantAmount = 0;
                licenceTempAmount = 0;
                compareResult = 0;
                licenceConstantHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceConstantGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceTempHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceTempGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);
                DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                if (n == 1)
                    sameMagreadCheckBox.Checked = false;

                if (n == 5)
                {
                    if ((magreadConstantAmountTextbox.Text != audioplayerConstantAmountTextbox.Text)
                                || (magreadTempAmountTextbox.Text != audioplayerTempAmountTextbox.Text))
                        sameMagreadCheckBox.Checked = false;
                    else
                        sameMagreadCheckBox.Checked = true;
                }

                if (licenceInfo.licences[n - 1].Version == 1)
                {
                    if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                        licenceInfo.licences[n - 1].ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                    if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                    {
                        DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                        compareResult = expirationDate.CompareTo(nowDate);

                        if (compareResult >= 0)
                        {
                            licenceInfo.licences[n - 1].TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                            licenceInfo.licences[n - 1].TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                        }
                    }
                }
                else
                {
                    foreach (XElement thirdLevel in secondLevel.Elements())
                    {
                        if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                        {
                            //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                            if (thirdLevel.Attribute("expiration_date") == null)
                            {
                                licenceInfo.licences[n - 1].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                    DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                    DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                licenceInfo.licences[n - 1].ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                            }
                            else
                            {
                                licenceInfo.licences[n - 1].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                    DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                    DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                compareResult = expirationDate.CompareTo(nowDate);
                                if (compareResult >= 0)
                                {
                                    licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    licenceInfo.licences[n - 1].TempAmountTotal.Text = licenceTempAmount.ToString();
                                }
                            }
                            licenceInfo.licences[n - 1].HistoryBtn.Enabled = true;
                        }
                    }
                }



/*                switch (n)
                {
                    case 1:
                        sameMagreadCheckBox.Checked = false;
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            magreadConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);

                            if (compareResult >= 0)
                            {
                                magreadTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                magreadTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 2:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            taskjournalConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);

                            if (compareResult >= 0)
                            {
                                taskjournalTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                taskjournalTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 3:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            magfaxConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);

                            if (compareResult >= 0)
                            {
                                magfaxTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                magfaxTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 4:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            trackingConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);

                            if (compareResult >= 0)
                            {
                                trackingTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                trackingTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 5:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            audioplayerConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);

                            if (compareResult >= 0)
                            {
                                audioplayerTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                audioplayerTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        if ((magreadConstantAmountTextbox.Text != audioplayerConstantAmountTextbox.Text)
                                || (magreadTempAmountTextbox.Text != audioplayerTempAmountTextbox.Text))
                            sameMagreadCheckBox.Checked = false;
                        else
                            sameMagreadCheckBox.Checked = true;

                        break;
                    case 6:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            billclientConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                billclientTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                billclientTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 7:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            mwieserverConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                mwieserverTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                mwieserverTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 8:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            mwieclientConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                mwieclientTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                mwieclientTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 9:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            mwiestationConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                mwiestationTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                mwiestationTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 10:
                        integrationserverConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                integrationserverTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                integrationserverTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 11:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            integrationadapterConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                integrationadapterTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                integrationadapterTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 12:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            videojournalConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                videojournalTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                videojournalTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 13:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            imitatorsormConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                imitatorsormTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                imitatorsormTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 14:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            maggradientConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                maggradientTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                maggradientTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 15:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            tracking3ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                tracking3TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                tracking3TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 16:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatemegafonConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatemegafonTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatemegafonTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 17:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatemtsConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatemtsTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatemtsTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 18:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatebeelineConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatebeelineTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatebeelineTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 19:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            istokclientConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                istokclientTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                istokclientTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 20:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            higinaconverterConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                higinaconverterTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                higinaconverterTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 21:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            webmapsConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                webmapsTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                webmapsTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 22:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gateyanvarConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gateyanvarTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gateyanvarTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 23:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            play3gvideoConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                play3gvideoTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                play3gvideoTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 24:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            integrationnetconfigConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                integrationnetconfigTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                integrationnetconfigTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 25:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[24].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    trackingg2ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[24].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        trackingg2TempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                trackingg2HistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 26:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[25].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    webmapsg2ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[25].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        webmapsg2TempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                webmapsg2HistoryBtn.Enabled = true;
                            }
                        }

                        break;
                    case 27:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[26].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    magreadg2ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[26].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        magreadg2TempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                magreadg2HistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 28:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[27].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    videojournalg2ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[27].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        videojournalg2TempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                videojournalg2HistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 29:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gateutkConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            gateutkTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                            gateutkTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                        }
                        break;
                    case 30:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[29].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    smsserviceConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[29].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        smsserviceTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                smsserviceHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 31:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[30].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    simserviceConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[30].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        simserviceTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                simserviceHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 32:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[31].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    barcodesadminConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[31].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        barcodesadminTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                barcodesadminHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 33:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatesmartsConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatesmartsTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatesmartsTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 34:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[33].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    simmagplaceserviceConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[33].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        simmagplaceserviceTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                simmagplaceserviceHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 35:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[34].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    armnvdConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[34].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        armnvdTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                armnvdHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 36:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[35].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    devicecontrolserverConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[35].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        devicecontrolserverTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                devicecontrolserverHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 37:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            trassatestConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                trassatestTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                trassatestTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 38:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            trassavkpruConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                trassavkpruTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                trassavkpruTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 39:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            trassavkputcpipConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                trassavkputcpipTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                trassavkputcpipTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 40:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            trassavkpputcpipConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                trassavkpputcpipTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                trassavkpputcpipTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 41:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gateservicevkConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gateservicevkTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gateservicevkTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 42:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            avk4ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                avk4TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                avk4TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    case 43:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[42].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    magbiyaconnectionConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[42].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        magbiyaconnectionTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                magbiyaconnectionHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 44:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[43].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    voiceidentificationConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[43].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        voiceidentificationTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                voiceidentificationHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 45:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[44].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    mobileobjectstrackingConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[44].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        mobileobjectstrackingTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                mobileobjectstrackingHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 46:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[45].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    barcodek2pConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[45].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        barcodek2pTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                barcodek2pHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 47:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[46].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    barcodek2rConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[46].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        barcodek2rTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                barcodek2rHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 48:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[47].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    barcodek2fConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[47].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        barcodek2fTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                barcodek2fHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 49:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[48].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    accountingbarcodeprintingConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[48].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        accountingbarcodeprintingTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                accountingbarcodeprintingHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    case 50:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[49].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    analyzestatisticserviceConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[49].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        analyzestatisticserviceTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                analyzestatisticserviceHistoryBtn.Enabled = true;
                            }
                        }
                        break;

                    case 51:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            avk2ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                avk2TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                avk2TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                    case 52:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            katunclientConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                katunclientTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                katunclientTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                    case 53:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatenssConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatenssTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatenssTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                        
                     case 54:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            avk2uConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                avk2uTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                avk2uTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                     case 55:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gateistokConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gateistokTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gateistokTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                     case 56:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[55].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    polyglotwebaccessConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[55].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        polyglotwebaccessTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                polyglotwebaccessHistoryBtn.Enabled = true;
                            }
                        }
                        break;

                    case 57:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[56].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    magservercomponentsConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[56].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        magservercomponentsTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                magservercomponentsHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    
                    case 58:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            rampaaudioConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                rampaaudioTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                rampaaudioTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                    case 59:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            gatetele2ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                gatetele2TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                gatetele2TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;

                    case 60:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            onvifdeviceConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                onvifdeviceTempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                onvifdeviceTempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;
                    
                     case 61:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[60].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    oracleserverbaseConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[60].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        oracleserverbaseTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                oracleserverbaseHistoryBtn.Enabled = true;
                            }
                        }
                        break;

                     case 62:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[61].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    oracleworkstationbaseConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[61].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        oracleworkstationbaseTempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                oracleworkstationbaseHistoryBtn.Enabled = true;
                            }
                        }
                        break;
                    
                    //здесь добавление инфы о новых лицензиях
                    /*
                     case xx:
                        if (int.Parse(secondLevel.Attribute("unlimited_licenses").Value) > 0)
                            ConstantAmountTextbox.Text = secondLevel.Attribute("unlimited_licenses").Value;
                        if (int.Parse(secondLevel.Attribute("limited_licenses").Value) > 0)
                        {
                            DateTime expirationDate = DateTime.Parse(secondLevel.Attribute("expiration_date").Value);
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                TempAmountTextbox.Text = secondLevel.Attribute("limited_licenses").Value;
                                TempDatebox.Text = secondLevel.Attribute("expiration_date").Value;
                            }
                        }
                        break;*/
                      
                    /*case xx:
                        foreach (XElement thirdLevel in secondLevel.Elements())
                        {
                            if (int.Parse(thirdLevel.Attribute("licenses").Value) != 0)
                            {
                                //извлекаем данные о всех прошивках данной лицензии, сохраняем в списке
                                if (thirdLevel.Attribute("expiration_date") == null)
                                {
                                    licenceInfo.licences[].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    licenceConstantAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                    ConstantAmountTotal.Text = licenceConstantAmount.ToString();
                                }
                                else
                                {
                                    licenceInfo.licences[].EntriesHistory.Add(new StelthXmlLicenceEntry(int.Parse(thirdLevel.Attribute("licenses").Value),
                                        DateTime.Parse(thirdLevel.Attribute("purchase_date").Value), DateTime.Parse(thirdLevel.Attribute("expiration_date").Value),
                                        DateTime.Parse(thirdLevel.Attribute("support_expiration_date").Value), DateTime.Parse(thirdLevel.Attribute("guarantee_expiration_date").Value)));

                                    DateTime expirationDate = DateTime.Parse(thirdLevel.Attribute("expiration_date").Value);
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        TempAmountTotal.Text = licenceTempAmount.ToString();
                                    }
                                }
                                HistoryBtn.Enabled = true;
                            }
                        }
                        break;
                     */
            //    }

            }
            #endregion

            #region обработка раздела "dongles_pack"

            firstLevel = newXml.Root.Element("dongles_pack");
            int pos = firstLevel.Attribute("id").Value.ToString().IndexOf('_');
            if (-1 == pos)
            {
                cityTextbox.Text = "";
                departmentTextbox.Text = "";
            }
            else
            {
                cityTextbox.Text = firstLevel.Attribute("id").Value.ToString().Substring(0, pos);
                departmentTextbox.Text = firstLevel.Attribute("id").Value.ToString().Substring(pos + 1);
            }

            XComment comment = null;
            XElement dongle = null;
            XNode node = firstLevel.FirstNode;

            mainKeyCheckBox.Checked = false;
            reserveKeyCheckBox.Checked = false;

            switch (node.NodeType)
            {
                case XmlNodeType.Comment:
                    comment = (XComment)node;
                    dongle = (XElement)comment.NextNode;
                    if (dongle.Attribute("type").Value == "master")
                    {
                        mainKeyIdTextbox.Text = dongle.Attribute("id").Value;
                        mainKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                        mainKeyNumberTextbox.Text = comment.Value.Trim();
                        mainKeyCheckBox.Checked = true;
                    }
                    if (dongle.Attribute("type").Value == "reserved")
                    {
                        reserveKeyIdTextbox.Text = dongle.Attribute("id").Value;
                        reserveKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                        reserveKeyNumberTextbox.Text = comment.Value.Trim();
                        reserveKeyCheckBox.Checked = true;
                    }
                    break;
                case XmlNodeType.Element:
                    dongle = (XElement)node;
                    if (dongle.Attribute("type").Value == "master")
                    {
                        mainKeyIdTextbox.Text = dongle.Attribute("id").Value;
                        mainKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                        mainKeyNumberTextbox.Text = "";
                        mainKeyCheckBox.Checked = true;
                    }
                    if (dongle.Attribute("type").Value == "reserved")
                    {
                        reserveKeyIdTextbox.Text = dongle.Attribute("id").Value;
                        reserveKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                        reserveKeyNumberTextbox.Text = "";
                        reserveKeyCheckBox.Checked = true;
                    }
                    break;
            }
            node = dongle.NextNode;
            if (node != null)
            {
                switch (node.NodeType)
                {
                    case XmlNodeType.Comment:
                        comment = (XComment)node;
                        dongle = (XElement)comment.NextNode;
                        if (dongle.Attribute("type").Value == "master")
                        {
                            mainKeyIdTextbox.Text = dongle.Attribute("id").Value;
                            mainKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                            mainKeyNumberTextbox.Text = comment.Value.Trim();
                            mainKeyCheckBox.Checked = true;
                        }
                        if (dongle.Attribute("type").Value == "reserved")
                        {
                            reserveKeyIdTextbox.Text = dongle.Attribute("id").Value;
                            reserveKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                            reserveKeyNumberTextbox.Text = comment.Value.Trim();
                            reserveKeyCheckBox.Checked = true;
                        }
                        break;
                    case XmlNodeType.Element:
                        dongle = (XElement)node;
                        if (dongle.Attribute("type").Value == "master")
                        {
                            mainKeyIdTextbox.Text = dongle.Attribute("id").Value;
                            mainKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                            mainKeyNumberTextbox.Text = "";
                            mainKeyCheckBox.Checked = true;
                        }
                        if (dongle.Attribute("type").Value == "reserved")
                        {
                            reserveKeyIdTextbox.Text = dongle.Attribute("id").Value;
                            reserveKeyCodeTextbox.Text = dongle.Attribute("TRU_cypher").Value;
                            reserveKeyNumberTextbox.Text = "";
                            reserveKeyCheckBox.Checked = true;
                        }
                        break;
                }

            }          
            #endregion

            #region обработка раздела "TRU_requests"

            firstLevel = newXml.Root.Element("TRU_requests");

            if (firstLevel != null)
            {
                XElement secondLevel = firstLevel.Element("TRU_request");
                if (mainKeyCheckBox.Checked)
                {
                    mainKeyRequestTextbox.Text = secondLevel.Attribute("request").Value;
                    if (reserveKeyCheckBox.Checked)
                    {
                        secondLevel = (XElement)(secondLevel.NextNode.NextNode);
                        reserveKeyRequestTextbox.Text = secondLevel.Attribute("request").Value;
                    }
                }
                else
                    if (reserveKeyCheckBox.Checked)
                        reserveKeyRequestTextbox.Text = secondLevel.Attribute("request").Value;
            }

            #endregion

            int i = newXmlName.LastIndexOf('\\');
            this.Text = MAIN_TITLE + newXmlName.Substring(i + 1);
            maintoolLicInfo.Enabled = true;
        }

        private void maintoolLicInfo_Click(object sender, EventArgs e)
        { 
            DataParser();
            for (int i = 0; i < StelthXml.LICENCES_AMOUNT; i++)
            {
                int licenceConstantAmount = 0;
                int licenceTempAmount = 0;

                //если лицензия версии 1
                if (1 == licenceInfo.licences[i].Version)
                {
                    if ((licenceInfo.licences[i].ConstantAmount > 0) && (licenceInfo.licences[i].TempAmount > 0))
                    {
                        resultText = resultText + licenceInfo.licences[i].RusName + "=" + licenceInfo.licences[i].ConstantAmount.ToString() + "; ";
                        resultText = resultText + licenceInfo.licences[i].RusName + "=" + licenceInfo.licences[i].TempAmount.ToString() + " (временные до " + licenceInfo.licences[i].ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                    }
                    else
                    {
                        if (licenceInfo.licences[i].ConstantAmount > 0)
                            resultText = resultText + licenceInfo.licences[i].RusName + "=" + licenceInfo.licences[i].ConstantAmount.ToString() + "; ";

                        if (licenceInfo.licences[i].TempAmount > 0)
                            resultText = resultText + licenceInfo.licences[i].RusName + "=" + licenceInfo.licences[i].TempAmount.ToString() + " (временные до " + licenceInfo.licences[i].ExpirationDate.ToString("dd.MM.yyyy") + "); ";

                    }
                }

                //если лицензия версии 2
                if ((2 == licenceInfo.licences[i].Version) && (licenceInfo.licences[i].EntriesHistory != null))
                {
                    
                    DateTime nowDate = new DateTime();
                    int compareResult = 0;

                    foreach (StelthXmlLicenceEntry entry in licenceInfo.licences[i].EntriesHistory)
                    {
                        DateTime expirationDate = entry.ExpirationDate;

                        if (!entry.IsTemp)
                            licenceConstantAmount += entry.Licenсes;

                        if (entry.IsTemp)
                        {
                            nowDate = DateTime.Now;
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                licenceTempAmount += entry.Licenсes;
                                resultText = resultText + licenceInfo.licences[i].RusName + "=" + entry.Licenсes.ToString() + " (временные до " + entry.ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                            }
                        }
                    }
                    if (licenceConstantAmount != 0)
                        resultText = resultText + licenceInfo.licences[i].RusName + "=" + licenceConstantAmount.ToString() + "; ";
                }
            }
            
            //открытие окна с результатом
            ResultForm resultForm = new ResultForm(resultText);
            resultForm.Text = RESULT_FORM_TITLE;
            resultForm.Show();
            resultText = "";
        }   
        #endregion

        #region СОХРАНЕНИЕ ФАЙЛА
        //проверка введённых значений и помещение их в память
        private void DataParser()
        {
            try
            {
                #region поля с лицензиями
                /*foreach (StelthXmlLicence licence in licenceInfo.licences)
                {
                    licence.ConstantAmount = 0;
                    licence.TempAmount = 0;
                }*/

                foreach (StelthXmlLicence licence in licenceInfo.licences)
                {
                    if (licence.Version == 1)
                    {
                        if (!String.IsNullOrEmpty(licence.ConstantAmountTextbox.Text))
                            licence.ConstantAmount = int.Parse(licence.ConstantAmountTextbox.Text);
                        if (!String.IsNullOrEmpty(licence.TempAmountTextbox.Text))
                        {
                            licence.TempAmount = int.Parse(licence.TempAmountTextbox.Text);
                            licence.ExpirationDate = licence.TempDatebox.Value;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(licence.ConstantAmountTextbox.Text))
                        {
                            StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(licence.ConstantAmountTextbox.Text),
                                dateBox.Value, licence.ConstantHelpdeskEndDatebox.Value, licence.ConstantGuaranteeEndDatebox.Value);
                            licence.EntryConstant = entry;
                        }
                        else
                            licence.EntryConstant = null;

                        if (!String.IsNullOrEmpty(licence.TempAmountTextbox.Text))
                        {
                            StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(licence.TempAmountTextbox.Text),
                                dateBox.Value, licence.TempDatebox.Value, licence.TempHelpdeskEndDatebox.Value, licence.TempGuaranteeEndDatebox.Value);
                            licence.EntryTemp = entry;
                        }
                        else
                            licence.EntryTemp = null;
                    }
                }
                

 /*               //1
                if (!String.IsNullOrEmpty(magreadConstantAmountTextbox.Text))
                    licenceInfo.licences[0].ConstantAmount = int.Parse(magreadConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(magreadTempAmountTextbox.Text))
                {
                    licenceInfo.licences[0].TempAmount = int.Parse(magreadTempAmountTextbox.Text);
                    licenceInfo.licences[0].ExpirationDate = magreadTempDatebox.Value;
                }

                //2
                if (!String.IsNullOrEmpty(taskjournalConstantAmountTextbox.Text))
                    licenceInfo.licences[1].ConstantAmount = int.Parse(taskjournalConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(taskjournalTempAmountTextbox.Text))
                {
                    licenceInfo.licences[1].TempAmount = int.Parse(taskjournalTempAmountTextbox.Text);
                    licenceInfo.licences[1].ExpirationDate = taskjournalTempDatebox.Value;
                }

                //3
                if (!String.IsNullOrEmpty(magfaxConstantAmountTextbox.Text))
                    licenceInfo.licences[2].ConstantAmount = int.Parse(magfaxConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(magfaxTempAmountTextbox.Text))
                {
                    licenceInfo.licences[2].TempAmount = int.Parse(magfaxTempAmountTextbox.Text);
                    licenceInfo.licences[2].ExpirationDate = magfaxTempDatebox.Value;
                }

                //4
                if (!String.IsNullOrEmpty(trackingConstantAmountTextbox.Text))
                    licenceInfo.licences[3].ConstantAmount = int.Parse(trackingConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(trackingTempAmountTextbox.Text))
                {
                    licenceInfo.licences[3].TempAmount = int.Parse(trackingTempAmountTextbox.Text);
                    licenceInfo.licences[3].ExpirationDate = trackingTempDatebox.Value;
                }

                //5
                if (!String.IsNullOrEmpty(audioplayerConstantAmountTextbox.Text))
                {
                    licenceInfo.licences[4].ConstantAmount = int.Parse(audioplayerConstantAmountTextbox.Text);
                }
                if (!String.IsNullOrEmpty(audioplayerTempAmountTextbox.Text))
                {
                    licenceInfo.licences[4].TempAmount = int.Parse(audioplayerTempAmountTextbox.Text);
                    licenceInfo.licences[4].ExpirationDate = audioplayerTempDatebox.Value;
                }

                //6
                if (!String.IsNullOrEmpty(billclientConstantAmountTextbox.Text))
                    licenceInfo.licences[5].ConstantAmount = int.Parse(billclientConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(billclientTempAmountTextbox.Text))
                {
                    licenceInfo.licences[5].TempAmount = int.Parse(billclientTempAmountTextbox.Text);
                    licenceInfo.licences[5].ExpirationDate = billclientTempDatebox.Value;
                }

                //7
                if (!String.IsNullOrEmpty(mwieserverConstantAmountTextbox.Text))
                    licenceInfo.licences[6].ConstantAmount = int.Parse(mwieserverConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(mwieserverTempAmountTextbox.Text))
                {
                    licenceInfo.licences[6].TempAmount = int.Parse(mwieserverTempAmountTextbox.Text);
                    licenceInfo.licences[6].ExpirationDate = mwieserverTempDatebox.Value;
                }

                //8
                if (!String.IsNullOrEmpty(mwieclientConstantAmountTextbox.Text))
                    licenceInfo.licences[7].ConstantAmount = int.Parse(mwieclientConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(mwieclientTempAmountTextbox.Text))
                {
                    licenceInfo.licences[7].TempAmount = int.Parse(mwieclientTempAmountTextbox.Text);
                    licenceInfo.licences[7].ExpirationDate = mwieclientTempDatebox.Value;
                }

                //9
                if (!String.IsNullOrEmpty(mwiestationConstantAmountTextbox.Text))
                    licenceInfo.licences[8].ConstantAmount = int.Parse(mwiestationConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(mwiestationTempAmountTextbox.Text))
                {
                    licenceInfo.licences[8].TempAmount = int.Parse(mwiestationTempAmountTextbox.Text);
                    licenceInfo.licences[8].ExpirationDate = mwiestationTempDatebox.Value;
                }

                //10
                if (!String.IsNullOrEmpty(integrationserverConstantAmountTextbox.Text))
                    licenceInfo.licences[9].ConstantAmount = int.Parse(integrationserverConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(integrationserverTempAmountTextbox.Text))
                {
                    licenceInfo.licences[9].TempAmount = int.Parse(integrationserverTempAmountTextbox.Text);
                    licenceInfo.licences[9].ExpirationDate = integrationserverTempDatebox.Value;
                }

                //11
                if (!String.IsNullOrEmpty(integrationadapterConstantAmountTextbox.Text))
                    licenceInfo.licences[10].ConstantAmount = int.Parse(integrationadapterConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(integrationadapterTempAmountTextbox.Text))
                {
                    licenceInfo.licences[10].TempAmount = int.Parse(integrationadapterTempAmountTextbox.Text);
                    licenceInfo.licences[10].ExpirationDate = integrationadapterTempDatebox.Value;
                }

                //12
                if (!String.IsNullOrEmpty(videojournalConstantAmountTextbox.Text))
                    licenceInfo.licences[11].ConstantAmount = int.Parse(videojournalConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(videojournalTempAmountTextbox.Text))
                {
                    licenceInfo.licences[11].TempAmount = int.Parse(videojournalTempAmountTextbox.Text);
                    licenceInfo.licences[11].ExpirationDate = videojournalTempDatebox.Value;
                }

                //13
                if (!String.IsNullOrEmpty(imitatorsormConstantAmountTextbox.Text))
                    licenceInfo.licences[12].ConstantAmount = int.Parse(imitatorsormConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(imitatorsormTempAmountTextbox.Text))
                {
                    licenceInfo.licences[12].TempAmount = int.Parse(imitatorsormTempAmountTextbox.Text);
                    licenceInfo.licences[12].ExpirationDate = imitatorsormTempDatebox.Value;
                }

                //14
                if (!String.IsNullOrEmpty(maggradientConstantAmountTextbox.Text))
                    licenceInfo.licences[13].ConstantAmount = int.Parse(maggradientConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(maggradientTempAmountTextbox.Text))
                {
                    licenceInfo.licences[13].TempAmount = int.Parse(maggradientTempAmountTextbox.Text);
                    licenceInfo.licences[13].ExpirationDate = maggradientTempDatebox.Value;
                }

                //15
                if (!String.IsNullOrEmpty(tracking3ConstantAmountTextbox.Text))
                    licenceInfo.licences[14].ConstantAmount = int.Parse(tracking3ConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(tracking3TempAmountTextbox.Text))
                {
                    licenceInfo.licences[14].TempAmount = int.Parse(tracking3TempAmountTextbox.Text);
                    licenceInfo.licences[14].ExpirationDate = tracking3TempDatebox.Value;
                }

                //16
                if (!String.IsNullOrEmpty(gatemegafonConstantAmountTextbox.Text))
                    licenceInfo.licences[15].ConstantAmount = int.Parse(gatemegafonConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatemegafonTempAmountTextbox.Text))
                {
                    licenceInfo.licences[15].TempAmount = int.Parse(gatemegafonTempAmountTextbox.Text);
                    licenceInfo.licences[15].ExpirationDate = gatemegafonTempDatebox.Value;
                }

                //17
                if (!String.IsNullOrEmpty(gatemtsConstantAmountTextbox.Text))
                    licenceInfo.licences[16].ConstantAmount = int.Parse(gatemtsConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatemtsTempAmountTextbox.Text))
                {
                    licenceInfo.licences[16].TempAmount = int.Parse(gatemtsTempAmountTextbox.Text);
                    licenceInfo.licences[16].ExpirationDate = gatemtsTempDatebox.Value;
                }

                //18
                if (!String.IsNullOrEmpty(gatebeelineConstantAmountTextbox.Text))
                    licenceInfo.licences[17].ConstantAmount = int.Parse(gatebeelineConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatebeelineTempAmountTextbox.Text))
                {
                    licenceInfo.licences[17].TempAmount = int.Parse(gatebeelineTempAmountTextbox.Text);
                    licenceInfo.licences[17].ExpirationDate = gatebeelineTempDatebox.Value;
                }

                //19
                if (!String.IsNullOrEmpty(istokclientConstantAmountTextbox.Text))
                    licenceInfo.licences[18].ConstantAmount = int.Parse(istokclientConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(istokclientTempAmountTextbox.Text))
                {
                    licenceInfo.licences[18].TempAmount = int.Parse(istokclientTempAmountTextbox.Text);
                    licenceInfo.licences[18].ExpirationDate = istokclientTempDatebox.Value;
                }

                //20
                if (!String.IsNullOrEmpty(higinaconverterConstantAmountTextbox.Text))
                    licenceInfo.licences[19].ConstantAmount = int.Parse(higinaconverterConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(higinaconverterTempAmountTextbox.Text))
                {
                    licenceInfo.licences[19].TempAmount = int.Parse(higinaconverterTempAmountTextbox.Text);
                    licenceInfo.licences[19].ExpirationDate = higinaconverterTempDatebox.Value;
                }

                //21
                if (!String.IsNullOrEmpty(webmapsConstantAmountTextbox.Text))
                    licenceInfo.licences[20].ConstantAmount = int.Parse(webmapsConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(webmapsTempAmountTextbox.Text))
                {
                    licenceInfo.licences[20].TempAmount = int.Parse(webmapsTempAmountTextbox.Text);
                    licenceInfo.licences[20].ExpirationDate = webmapsTempDatebox.Value;
                }

                //22
                if (!String.IsNullOrEmpty(gateyanvarConstantAmountTextbox.Text))
                    licenceInfo.licences[21].ConstantAmount = int.Parse(gateyanvarConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gateyanvarTempAmountTextbox.Text))
                {
                    licenceInfo.licences[21].TempAmount = int.Parse(gateyanvarTempAmountTextbox.Text);
                    licenceInfo.licences[21].ExpirationDate = gateyanvarTempDatebox.Value;
                }

                //23
                if (!String.IsNullOrEmpty(play3gvideoConstantAmountTextbox.Text))
                    licenceInfo.licences[22].ConstantAmount = int.Parse(play3gvideoConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(play3gvideoTempAmountTextbox.Text))
                {
                    licenceInfo.licences[22].TempAmount = int.Parse(play3gvideoTempAmountTextbox.Text);
                    licenceInfo.licences[22].ExpirationDate = play3gvideoTempDatebox.Value;
                }

                //24
                if (!String.IsNullOrEmpty(integrationnetconfigConstantAmountTextbox.Text))
                    licenceInfo.licences[23].ConstantAmount = int.Parse(integrationnetconfigConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(integrationnetconfigTempAmountTextbox.Text))
                {
                    licenceInfo.licences[23].TempAmount = int.Parse(integrationnetconfigTempAmountTextbox.Text);
                    licenceInfo.licences[23].ExpirationDate = integrationnetconfigTempDatebox.Value;
                }

                //25
                if (!String.IsNullOrEmpty(trackingg2ConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(trackingg2ConstantAmountTextbox.Text),
                        dateBox.Value, trackingg2ConstantHelpdeskEndDatebox.Value, trackingg2ConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[24].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[24].EntryConstant = null;

                if (!String.IsNullOrEmpty(trackingg2TempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(trackingg2TempAmountTextbox.Text),
                        dateBox.Value, trackingg2TempDatebox.Value, trackingg2TempHelpdeskEndDatebox.Value, trackingg2TempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[24].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[24].EntryTemp = null;

                //26
                if (!String.IsNullOrEmpty(webmapsg2ConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(webmapsg2ConstantAmountTextbox.Text),
                        dateBox.Value, webmapsg2ConstantHelpdeskEndDatebox.Value, webmapsg2ConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[25].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[25].EntryConstant = null;

                if (!String.IsNullOrEmpty(webmapsg2TempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(webmapsg2TempAmountTextbox.Text),
                        dateBox.Value, webmapsg2TempDatebox.Value, webmapsg2TempHelpdeskEndDatebox.Value, webmapsg2TempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[25].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[25].EntryTemp = null;

                //27
                if (!String.IsNullOrEmpty(magreadg2ConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magreadg2ConstantAmountTextbox.Text),
                        dateBox.Value, magreadg2ConstantHelpdeskEndDatebox.Value, magreadg2ConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[26].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[26].EntryConstant = null;

                if (!String.IsNullOrEmpty(magreadg2TempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magreadg2TempAmountTextbox.Text),
                        dateBox.Value, magreadg2TempDatebox.Value, magreadg2TempHelpdeskEndDatebox.Value, magreadg2TempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[26].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[26].EntryTemp = null;

                //28
                if (!String.IsNullOrEmpty(videojournalg2ConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(videojournalg2ConstantAmountTextbox.Text),
                        dateBox.Value, videojournalg2ConstantHelpdeskEndDatebox.Value, videojournalg2ConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[27].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[27].EntryConstant = null;

                if (!String.IsNullOrEmpty(videojournalg2TempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(videojournalg2TempAmountTextbox.Text),
                        dateBox.Value, videojournalg2TempDatebox.Value, videojournalg2TempHelpdeskEndDatebox.Value, videojournalg2TempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[27].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[27].EntryTemp = null;

                //29
                if (!String.IsNullOrEmpty(gateutkConstantAmountTextbox.Text))
                    licenceInfo.licences[28].ConstantAmount = int.Parse(gateutkConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gateutkTempAmountTextbox.Text))
                {
                    licenceInfo.licences[28].TempAmount = int.Parse(gateutkTempAmountTextbox.Text);
                    licenceInfo.licences[28].ExpirationDate = gateutkTempDatebox.Value;
                }

                //30
                if (!String.IsNullOrEmpty(smsserviceConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(smsserviceConstantAmountTextbox.Text),
                        dateBox.Value, smsserviceConstantHelpdeskEndDatebox.Value, smsserviceConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[29].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[29].EntryConstant = null;

                if (!String.IsNullOrEmpty(smsserviceTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(smsserviceTempAmountTextbox.Text),
                        dateBox.Value, smsserviceTempDatebox.Value, smsserviceTempHelpdeskEndDatebox.Value, smsserviceTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[29].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[29].EntryTemp = null;

                //31
                if (!String.IsNullOrEmpty(simserviceConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(simserviceConstantAmountTextbox.Text),
                        dateBox.Value, simserviceConstantHelpdeskEndDatebox.Value, simserviceConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[30].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[30].EntryConstant = null;


                if (!String.IsNullOrEmpty(simserviceTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(simserviceTempAmountTextbox.Text),
                        dateBox.Value, simserviceTempDatebox.Value, simserviceTempHelpdeskEndDatebox.Value, simserviceTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[30].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[30].EntryTemp = null;

                //32
                if (!String.IsNullOrEmpty(barcodesadminConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodesadminConstantAmountTextbox.Text),
                        dateBox.Value, barcodesadminConstantHelpdeskEndDatebox.Value, barcodesadminConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[31].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[31].EntryConstant = null;

                if (!String.IsNullOrEmpty(barcodesadminTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodesadminTempAmountTextbox.Text),
                        dateBox.Value, barcodesadminTempDatebox.Value, barcodesadminTempHelpdeskEndDatebox.Value, barcodesadminTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[31].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[31].EntryTemp = null;

                //33
                if (!String.IsNullOrEmpty(gatesmartsConstantAmountTextbox.Text))
                    licenceInfo.licences[32].ConstantAmount = int.Parse(gatesmartsConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatesmartsTempAmountTextbox.Text))
                {
                    licenceInfo.licences[32].TempAmount = int.Parse(gatesmartsTempAmountTextbox.Text);
                    licenceInfo.licences[32].ExpirationDate = gatesmartsTempDatebox.Value;
                }

                //34
                if (!String.IsNullOrEmpty(simmagplaceserviceConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(simmagplaceserviceConstantAmountTextbox.Text),
                        dateBox.Value, simmagplaceserviceConstantHelpdeskEndDatebox.Value, simmagplaceserviceConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[33].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[33].EntryConstant = null;

                if (!String.IsNullOrEmpty(simmagplaceserviceTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(simmagplaceserviceTempAmountTextbox.Text),
                        dateBox.Value, simmagplaceserviceTempDatebox.Value, simmagplaceserviceTempHelpdeskEndDatebox.Value, simmagplaceserviceTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[33].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[33].EntryTemp = null;

                //35
                if (!String.IsNullOrEmpty(armnvdConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(armnvdConstantAmountTextbox.Text),
                        dateBox.Value, armnvdConstantHelpdeskEndDatebox.Value, armnvdConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[34].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[34].EntryConstant = null;

                if (!String.IsNullOrEmpty(armnvdTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(armnvdTempAmountTextbox.Text),
                        dateBox.Value, armnvdTempDatebox.Value, armnvdTempHelpdeskEndDatebox.Value, armnvdTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[34].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[34].EntryTemp = null;

                //36
                if (!String.IsNullOrEmpty(devicecontrolserverConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(devicecontrolserverConstantAmountTextbox.Text),
                        dateBox.Value, devicecontrolserverConstantHelpdeskEndDatebox.Value, devicecontrolserverConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[35].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[35].EntryConstant = null;

                if (!String.IsNullOrEmpty(devicecontrolserverTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(devicecontrolserverTempAmountTextbox.Text),
                        dateBox.Value, devicecontrolserverTempDatebox.Value, devicecontrolserverTempHelpdeskEndDatebox.Value, devicecontrolserverTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[35].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[35].EntryTemp = null;

                //37
                if (!String.IsNullOrEmpty(trassatestConstantAmountTextbox.Text))
                    licenceInfo.licences[36].ConstantAmount = int.Parse(trassatestConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(trassatestTempAmountTextbox.Text))
                {
                    licenceInfo.licences[36].TempAmount = int.Parse(trassatestTempAmountTextbox.Text);
                    licenceInfo.licences[36].ExpirationDate = trassatestTempDatebox.Value;
                }

                //38
                if (!String.IsNullOrEmpty(trassavkpruConstantAmountTextbox.Text))
                    licenceInfo.licences[37].ConstantAmount = int.Parse(trassavkpruConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(trassavkpruTempAmountTextbox.Text))
                {
                    licenceInfo.licences[37].TempAmount = int.Parse(trassavkpruTempAmountTextbox.Text);
                    licenceInfo.licences[37].ExpirationDate = trassavkpruTempDatebox.Value;
                }

                //39
                if (!String.IsNullOrEmpty(trassavkputcpipConstantAmountTextbox.Text))
                    licenceInfo.licences[38].ConstantAmount = int.Parse(trassavkputcpipConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(trassavkputcpipTempAmountTextbox.Text))
                {
                    licenceInfo.licences[38].TempAmount = int.Parse(trassavkputcpipTempAmountTextbox.Text);
                    licenceInfo.licences[38].ExpirationDate = trassavkputcpipTempDatebox.Value;
                }

                //40
                if (!String.IsNullOrEmpty(trassavkpputcpipConstantAmountTextbox.Text))
                    licenceInfo.licences[39].ConstantAmount = int.Parse(trassavkpputcpipConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(trassavkpputcpipTempAmountTextbox.Text))
                {
                    licenceInfo.licences[39].TempAmount = int.Parse(trassavkpputcpipTempAmountTextbox.Text);
                    licenceInfo.licences[39].ExpirationDate = trassavkpputcpipTempDatebox.Value;
                }

                //41
                if (!String.IsNullOrEmpty(gateservicevkConstantAmountTextbox.Text))
                    licenceInfo.licences[40].ConstantAmount = int.Parse(gateservicevkConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gateservicevkTempAmountTextbox.Text))
                {
                    licenceInfo.licences[40].TempAmount = int.Parse(gateservicevkTempAmountTextbox.Text);
                    licenceInfo.licences[40].ExpirationDate = gateservicevkTempDatebox.Value;
                }

                //42
                if (!String.IsNullOrEmpty(avk4ConstantAmountTextbox.Text))
                    licenceInfo.licences[41].ConstantAmount = int.Parse(avk4ConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(avk4TempAmountTextbox.Text))
                {
                    licenceInfo.licences[41].TempAmount = int.Parse(avk4TempAmountTextbox.Text);
                    licenceInfo.licences[41].ExpirationDate = avk4TempDatebox.Value;
                }

                //43
                if (!String.IsNullOrEmpty(magbiyaconnectionConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magbiyaconnectionConstantAmountTextbox.Text),
                        dateBox.Value, magbiyaconnectionConstantHelpdeskEndDatebox.Value, magbiyaconnectionConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[42].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[42].EntryConstant = null;

                if (!String.IsNullOrEmpty(magbiyaconnectionTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magbiyaconnectionTempAmountTextbox.Text),
                        dateBox.Value, magbiyaconnectionTempDatebox.Value, magbiyaconnectionTempHelpdeskEndDatebox.Value, magbiyaconnectionTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[42].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[42].EntryTemp = null;

                //44
                if (!String.IsNullOrEmpty(voiceidentificationConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(voiceidentificationConstantAmountTextbox.Text),
                        dateBox.Value, voiceidentificationConstantHelpdeskEndDatebox.Value, voiceidentificationConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[43].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[43].EntryConstant = null;

                if (!String.IsNullOrEmpty(voiceidentificationTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(voiceidentificationTempAmountTextbox.Text),
                        dateBox.Value, voiceidentificationTempDatebox.Value, voiceidentificationTempHelpdeskEndDatebox.Value, voiceidentificationTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[43].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[43].EntryTemp = null;

                //45
                if (!String.IsNullOrEmpty(mobileobjectstrackingConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(mobileobjectstrackingConstantAmountTextbox.Text),
                        dateBox.Value, mobileobjectstrackingConstantHelpdeskEndDatebox.Value, mobileobjectstrackingConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[44].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[44].EntryConstant = null;

                if (!String.IsNullOrEmpty(mobileobjectstrackingTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(mobileobjectstrackingTempAmountTextbox.Text),
                        dateBox.Value, mobileobjectstrackingTempDatebox.Value, mobileobjectstrackingTempHelpdeskEndDatebox.Value, mobileobjectstrackingTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[44].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[44].EntryTemp = null;

                //46
                if (!String.IsNullOrEmpty(barcodek2pConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2pConstantAmountTextbox.Text),
                        dateBox.Value, barcodek2pConstantHelpdeskEndDatebox.Value, barcodek2pConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[45].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[45].EntryConstant = null;

                if (!String.IsNullOrEmpty(barcodek2pTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2pTempAmountTextbox.Text),
                        dateBox.Value, barcodek2pTempDatebox.Value, barcodek2pTempHelpdeskEndDatebox.Value, barcodek2pTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[45].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[45].EntryTemp = null;

                //47
                if (!String.IsNullOrEmpty(barcodek2rConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2rConstantAmountTextbox.Text),
                        dateBox.Value, barcodek2rConstantHelpdeskEndDatebox.Value, barcodek2rConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[46].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[46].EntryConstant = null;

                if (!String.IsNullOrEmpty(barcodek2rTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2rTempAmountTextbox.Text),
                        dateBox.Value, barcodek2rTempDatebox.Value, barcodek2rTempHelpdeskEndDatebox.Value, barcodek2rTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[46].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[46].EntryTemp = null;

                //48
                if (!String.IsNullOrEmpty(barcodek2fConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2fConstantAmountTextbox.Text),
                        dateBox.Value, barcodek2fConstantHelpdeskEndDatebox.Value, barcodek2fConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[47].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[47].EntryConstant = null;

                if (!String.IsNullOrEmpty(barcodek2fTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(barcodek2fTempAmountTextbox.Text),
                        dateBox.Value, barcodek2fTempDatebox.Value, barcodek2fTempHelpdeskEndDatebox.Value, barcodek2fTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[47].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[47].EntryTemp = null;

                //49
                if (!String.IsNullOrEmpty(accountingbarcodeprintingConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(accountingbarcodeprintingConstantAmountTextbox.Text),
                        dateBox.Value, accountingbarcodeprintingConstantHelpdeskEndDatebox.Value, accountingbarcodeprintingConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[48].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[48].EntryConstant = null;

                if (!String.IsNullOrEmpty(accountingbarcodeprintingTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(accountingbarcodeprintingTempAmountTextbox.Text),
                        dateBox.Value, accountingbarcodeprintingTempDatebox.Value, accountingbarcodeprintingTempHelpdeskEndDatebox.Value, accountingbarcodeprintingTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[48].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[48].EntryTemp = null;

                //50
                if (!String.IsNullOrEmpty(analyzestatisticserviceConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(analyzestatisticserviceConstantAmountTextbox.Text),
                        dateBox.Value, analyzestatisticserviceConstantHelpdeskEndDatebox.Value, analyzestatisticserviceConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[49].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[49].EntryConstant = null;

                if (!String.IsNullOrEmpty(analyzestatisticserviceTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(analyzestatisticserviceTempAmountTextbox.Text),
                        dateBox.Value, analyzestatisticserviceTempDatebox.Value, analyzestatisticserviceTempHelpdeskEndDatebox.Value, analyzestatisticserviceTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[49].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[49].EntryTemp = null;

                //51
                if (!String.IsNullOrEmpty(avk2ConstantAmountTextbox.Text))
                    licenceInfo.licences[50].ConstantAmount = int.Parse(avk2ConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(avk2TempAmountTextbox.Text))
                {
                    licenceInfo.licences[50].TempAmount = int.Parse(avk2TempAmountTextbox.Text);
                    licenceInfo.licences[50].ExpirationDate = avk2TempDatebox.Value;
                }

                //52
                if (!String.IsNullOrEmpty(katunclientConstantAmountTextbox.Text))
                    licenceInfo.licences[51].ConstantAmount = int.Parse(katunclientConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(katunclientTempAmountTextbox.Text))
                {
                    licenceInfo.licences[51].TempAmount = int.Parse(katunclientTempAmountTextbox.Text);
                    licenceInfo.licences[51].ExpirationDate = katunclientTempDatebox.Value;
                }

                //53
                if (!String.IsNullOrEmpty(gatenssConstantAmountTextbox.Text))
                    licenceInfo.licences[52].ConstantAmount = int.Parse(gatenssConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatenssTempAmountTextbox.Text))
                {
                    licenceInfo.licences[52].TempAmount = int.Parse(gatenssTempAmountTextbox.Text);
                    licenceInfo.licences[52].ExpirationDate = gatenssTempDatebox.Value;
                }
    
                //54
                if (!String.IsNullOrEmpty(avk2uConstantAmountTextbox.Text))
                    licenceInfo.licences[53].ConstantAmount = int.Parse(avk2uConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(avk2uTempAmountTextbox.Text))
                {
                    licenceInfo.licences[53].TempAmount = int.Parse(avk2uTempAmountTextbox.Text);
                    licenceInfo.licences[53].ExpirationDate = avk2uTempDatebox.Value;
                }

                //55
                if (!String.IsNullOrEmpty(gateistokConstantAmountTextbox.Text))
                    licenceInfo.licences[54].ConstantAmount = int.Parse(gateistokConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gateistokTempAmountTextbox.Text))
                {
                    licenceInfo.licences[54].TempAmount = int.Parse(gateistokTempAmountTextbox.Text);
                    licenceInfo.licences[54].ExpirationDate = gateistokTempDatebox.Value;
                }

                //56
                if (!String.IsNullOrEmpty(polyglotwebaccessConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(polyglotwebaccessConstantAmountTextbox.Text),
                        dateBox.Value, polyglotwebaccessConstantHelpdeskEndDatebox.Value, polyglotwebaccessConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[55].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[55].EntryConstant = null;

                if (!String.IsNullOrEmpty(polyglotwebaccessTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(polyglotwebaccessTempAmountTextbox.Text),
                        dateBox.Value, polyglotwebaccessTempDatebox.Value, polyglotwebaccessTempHelpdeskEndDatebox.Value, polyglotwebaccessTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[55].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[55].EntryTemp = null;

                //57
                if (!String.IsNullOrEmpty(magservercomponentsConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magservercomponentsConstantAmountTextbox.Text),
                        dateBox.Value, magservercomponentsConstantHelpdeskEndDatebox.Value, magservercomponentsConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[56].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[56].EntryConstant = null;

                if (!String.IsNullOrEmpty(magservercomponentsTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(magservercomponentsTempAmountTextbox.Text),
                        dateBox.Value, magservercomponentsTempDatebox.Value, magservercomponentsTempHelpdeskEndDatebox.Value, magservercomponentsTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[56].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[56].EntryTemp = null;
                
                //58
                if (!String.IsNullOrEmpty(rampaaudioConstantAmountTextbox.Text))
                    licenceInfo.licences[57].ConstantAmount = int.Parse(rampaaudioConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(rampaaudioTempAmountTextbox.Text))
                {
                    licenceInfo.licences[57].TempAmount = int.Parse(rampaaudioTempAmountTextbox.Text);
                    licenceInfo.licences[57].ExpirationDate = rampaaudioTempDatebox.Value;
                }
                
                //59
                if (!String.IsNullOrEmpty(gatetele2ConstantAmountTextbox.Text))
                    licenceInfo.licences[58].ConstantAmount = int.Parse(gatetele2ConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(gatetele2TempAmountTextbox.Text))
                {
                    licenceInfo.licences[58].TempAmount = int.Parse(gatetele2TempAmountTextbox.Text);
                    licenceInfo.licences[58].ExpirationDate = gatetele2TempDatebox.Value;
                }

                //60
                if (!String.IsNullOrEmpty(onvifdeviceConstantAmountTextbox.Text))
                    licenceInfo.licences[59].ConstantAmount = int.Parse(onvifdeviceConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(onvifdeviceTempAmountTextbox.Text))
                {
                    licenceInfo.licences[59].TempAmount = int.Parse(onvifdeviceTempAmountTextbox.Text);
                    licenceInfo.licences[59].ExpirationDate = onvifdeviceTempDatebox.Value;
                }

                //61
                if (!String.IsNullOrEmpty(oracleserverbaseConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(oracleserverbaseConstantAmountTextbox.Text),
                        dateBox.Value, oracleserverbaseConstantHelpdeskEndDatebox.Value, oracleserverbaseConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[60].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[60].EntryConstant = null;

                if (!String.IsNullOrEmpty(oracleserverbaseTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(oracleserverbaseTempAmountTextbox.Text),
                        dateBox.Value, oracleserverbaseTempDatebox.Value, oracleserverbaseTempHelpdeskEndDatebox.Value, oracleserverbaseTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[60].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[60].EntryTemp = null;

                //62
                if (!String.IsNullOrEmpty(oracleworkstationbaseConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(oracleworkstationbaseConstantAmountTextbox.Text),
                        dateBox.Value, oracleworkstationbaseConstantHelpdeskEndDatebox.Value, oracleworkstationbaseConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[61].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[61].EntryConstant = null;

                if (!String.IsNullOrEmpty(oracleworkstationbaseTempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(oracleworkstationbaseTempAmountTextbox.Text),
                        dateBox.Value, oracleworkstationbaseTempDatebox.Value, oracleworkstationbaseTempHelpdeskEndDatebox.Value, oracleworkstationbaseTempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[61].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[61].EntryTemp = null;*/
                
                //здесь добавление инфы о новых лицензиях
                /*
                //
                if (!String.IsNullOrEmpty(ConstantAmountTextbox.Text))
                    licenceInfo.licences[].ConstantAmount = int.Parse(ConstantAmountTextbox.Text);
                if (!String.IsNullOrEmpty(TempAmountTextbox.Text))
                {
                    licenceInfo.licences[].TempAmount = int.Parse(TempAmountTextbox.Text);
                    licenceInfo.licences[].ExpirationDate = TempDatebox.Value;
                }
                 */
                /*
                //
                if (!String.IsNullOrEmpty(ConstantAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(ConstantAmountTextbox.Text),
                        dateBox.Value, ConstantHelpdeskEndDatebox.Value, ConstantGuaranteeEndDatebox.Value);
                    licenceInfo.licences[].EntryConstant = entry;
                }
                else
                    licenceInfo.licences[].EntryConstant = null;

                if (!String.IsNullOrEmpty(TempAmountTextbox.Text))
                {
                    StelthXmlLicenceEntry entry = new StelthXmlLicenceEntry(int.Parse(TempAmountTextbox.Text),
                        dateBox.Value, TempDatebox.Value, TempHelpdeskEndDatebox.Value, TempGuaranteeEndDatebox.Value);
                    licenceInfo.licences[].EntryTemp = entry;
                }
                else
                    licenceInfo.licences[].EntryTemp = null;
                */

                #endregion поля с лицензиями

                #region проверка поля с названием города
                if (String.IsNullOrEmpty(cityTextbox.Text))
                {
                    throw new Exception(ERROR_UNKNOWN_CITY);
                }
                #endregion проверка поля с названием города

                #region проверка поля с названием ведомства
                if (String.IsNullOrEmpty(departmentTextbox.Text))
                {
                    throw new Exception(ERROR_UNKNOWN_DEPARTMENT);
                }
                #endregion проверка поля с названием ведомтсва

                #region проверка данных о ключах

                if (mainKeyCheckBox.Checked)
                {
                    if (String.IsNullOrEmpty(mainKeyNumberTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_MAIN_KEY_NUM);
                    }

                    if (String.IsNullOrEmpty(mainKeyIdTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_MAIN_KEY_ID);
                    }

                    if (String.IsNullOrEmpty(mainKeyCodeTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_MAIN_KEY_CODE);
                    }
                }
                if (reserveKeyCheckBox.Checked)
                {
                    if (String.IsNullOrEmpty(reserveKeyNumberTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_RESERVE_KEY_NUM);
                    }

                    if (String.IsNullOrEmpty(reserveKeyIdTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_RESERVE_KEY_ID);
                    }

                    if (String.IsNullOrEmpty(reserveKeyCodeTextbox.Text))
                    {
                        throw new Exception(ERROR_UNKNOWN_RESERVE_KEY_CODE);
                    }
                }
                #endregion проверка данных о ключах
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, ERROR_TITLE, MessageBoxButtons.OK);
            }
        }


        //заполнение нового xml-файла
        private int newXmlFilling()
        {
            #region обработка раздела "license_pack"

            resultText = "";

            int licenceConstantAmount = 0;
            int licenceTempAmount = 0;
            int compareResult = 0;
            DateTime licenceConstantHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceConstantGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceTempHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
            DateTime licenceTempGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);

            XElement firstLevel = newXml.Root.Element("license_pack");

            //установка даты
            firstLevel.Attribute("id").SetValue(dateBox.Value.ToString("dd.MM.yyyy"));

            //установка количества лицензий, удаление пустых блоков
            XElement nullLicBlock = null;
            foreach (XElement secondLevel in firstLevel.Elements())
            {
                licenceConstantAmount = 0;
                licenceTempAmount = 0;
                compareResult = 0;
                licenceConstantHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceConstantGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceTempHelpdeskEndDate = DateTime.Parse(DEFAULT_DATE);
                licenceTempGuaranteeEndDate = DateTime.Parse(DEFAULT_DATE);

                DateTime nowDate = new DateTime();
                
                int n = int.Parse(secondLevel.Attribute("product_id").Value);
                if (nullLicBlock != null)
                {
                    //удаление коммента
                    XNode comment = nullLicBlock.PreviousNode;
                    comment.Remove();
                    //удаление пустого блока лицензий
                    nullLicBlock.Remove();
                    nullLicBlock = null;
                }

                //если лицензия версии 1
                if (1 == licenceInfo.licences[n - 1].Version)
                {
                    if ((licenceInfo.licences[n - 1].ConstantAmount > 0) && (licenceInfo.licences[n - 1].TempAmount > 0))
                    {
                        secondLevel.Attribute("unlimited_licenses").SetValue(licenceInfo.licences[n - 1].ConstantAmount);
                        resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceInfo.licences[n - 1].ConstantAmount.ToString() + "; ";

                        secondLevel.Attribute("limited_licenses").SetValue(licenceInfo.licences[n - 1].TempAmount);
                        secondLevel.Attribute("expiration_date").SetValue(licenceInfo.licences[n - 1].ExpirationDate.ToString("yyyy.MM.dd"));
                        resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceInfo.licences[n - 1].TempAmount.ToString() + " (временные до " + licenceInfo.licences[n - 1].ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                    }
                    else
                    {
                        if ((licenceInfo.licences[n - 1].ConstantAmount == 0) && (licenceInfo.licences[n - 1].TempAmount == 0))
                        {
                            nullLicBlock = secondLevel;
                            continue;
                        }

                        if (licenceInfo.licences[n - 1].ConstantAmount > 0)
                        {
                            secondLevel.Attribute("unlimited_licenses").SetValue(licenceInfo.licences[n - 1].ConstantAmount);
                            resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceInfo.licences[n - 1].ConstantAmount.ToString() + "; ";
                        }

                        if (licenceInfo.licences[n - 1].TempAmount > 0)
                        {
                            secondLevel.Attribute("limited_licenses").SetValue(licenceInfo.licences[n - 1].TempAmount);
                            secondLevel.Attribute("expiration_date").SetValue(licenceInfo.licences[n - 1].ExpirationDate.ToString("yyyy.MM.dd"));
                            resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceInfo.licences[n - 1].TempAmount.ToString() + " (временные до " + licenceInfo.licences[n - 1].ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                        }

                    }
                }

                //если лицензия версии 2
                if (2 == licenceInfo.licences[n - 1].Version)
                {
                    //удаление пустого блока если нет прежних записей и нет текущей
                    if ((licenceInfo.licences[n - 1].EntriesHistory.Count == 0)
                        && (licenceInfo.licences[n - 1].EntryConstant == null)
                        && (licenceInfo.licences[n - 1].EntryTemp == null))
                    {
                        nullLicBlock = secondLevel;
                        continue;
                    }
                    else
                    {
                        //заполнение прежней истории
                        if (licenceInfo.licences[n - 1].EntriesHistory != null)
                        {
                            foreach (StelthXmlLicenceEntry entry in licenceInfo.licences[n - 1].EntriesHistory)
                            {
                                XElement thirdLevel = new XElement("entry");
                                thirdLevel.Add(new XAttribute("licenses", entry.Licenсes));
                                if ((n == 27) && ((licenceInfo.licences[56].EntriesHistory.Count > 0) 
                                    || (licenceInfo.licences[56].EntryConstant != null)
                                    || (licenceInfo.licences[56].EntryTemp != null)))
                                    thirdLevel.Add(new XAttribute("sharing-policy", "terminal-wide"));
                                thirdLevel.Add(new XAttribute("purchase_date", entry.PurchaseDate.ToString("yyyy.MM.dd")));
                                thirdLevel.Add(new XAttribute("support_expiration_date", entry.SupportExpirationDate.ToString("yyyy.MM.dd")));
                                thirdLevel.Add(new XAttribute("guarantee_expiration_date", entry.GuaranteeExpirationDate.ToString("yyyy.MM.dd")));
                                if (entry.IsTemp)
                                    thirdLevel.Add(new XAttribute("expiration_date", entry.ExpirationDate.ToString("yyyy.MM.dd")));
                                secondLevel.Add(thirdLevel);

                                if (!entry.IsTemp)
                                    licenceConstantAmount += entry.Licenсes;

                                if (entry.IsTemp)
                                {
                                    DateTime expirationDate = entry.ExpirationDate;
                                    nowDate = DateTime.Now;
                                    compareResult = expirationDate.CompareTo(nowDate);
                                    if (compareResult >= 0)
                                    {
                                        licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                        resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + entry.Licenсes.ToString() + " (временные до " + entry.ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                                    }
                                }
                                
                            }
                        }

                        //заполнение новой записи о временных лицензиях
                        if ((licenceInfo.licences[n - 1].EntryTemp != null) && (licenceInfo.licences[n - 1].EntryTemp.Licenсes > 0))
                        {
                            XElement thirdLevel = new XElement("entry");
                            thirdLevel.Add(new XAttribute("licenses", licenceInfo.licences[n - 1].EntryTemp.Licenсes));
                            if ((n == 27) && ((licenceInfo.licences[56].EntriesHistory.Count > 0)
                                   || (licenceInfo.licences[56].EntryConstant != null)
                                   || (licenceInfo.licences[56].EntryTemp != null)))
                                thirdLevel.Add(new XAttribute("sharing-policy", "terminal-wide"));
                            thirdLevel.Add(new XAttribute("purchase_date", licenceInfo.licences[n - 1].EntryTemp.PurchaseDate.ToString("yyyy.MM.dd")));
                            thirdLevel.Add(new XAttribute("support_expiration_date", licenceInfo.licences[n - 1].EntryTemp.SupportExpirationDate.ToString("yyyy.MM.dd")));
                            thirdLevel.Add(new XAttribute("guarantee_expiration_date", licenceInfo.licences[n - 1].EntryTemp.GuaranteeExpirationDate.ToString("yyyy.MM.dd")));
                            thirdLevel.Add(new XAttribute("expiration_date", licenceInfo.licences[n - 1].EntryTemp.ExpirationDate.ToString("yyyy.MM.dd")));
                            secondLevel.Add(thirdLevel);

                            DateTime expirationDate = DateTime.Parse(licenceInfo.licences[n - 1].EntryTemp.ExpirationDate.ToString("yyyy.MM.dd"));
                            nowDate = DateTime.Now;
                            
                            compareResult = expirationDate.CompareTo(nowDate);
                            if (compareResult >= 0)
                            {
                                licenceTempAmount += int.Parse(thirdLevel.Attribute("licenses").Value);
                                resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceInfo.licences[n - 1].EntryTemp.Licenсes.ToString() + " (временные до " + licenceInfo.licences[n - 1].EntryTemp.ExpirationDate.ToString("dd.MM.yyyy") + "); ";
                            }
                        }

                        //заполнение новой записи о постоянных лицензиях
                        if ((licenceInfo.licences[n - 1].EntryConstant != null) && (licenceInfo.licences[n - 1].EntryConstant.Licenсes > 0))
                        {
                            XElement thirdLevel = new XElement("entry");
                            thirdLevel.Add(new XAttribute("licenses", licenceInfo.licences[n - 1].EntryConstant.Licenсes));
                            if ((n == 27) && ((licenceInfo.licences[56].EntriesHistory.Count > 0)
                                   || (licenceInfo.licences[56].EntryConstant != null)
                                   || (licenceInfo.licences[56].EntryTemp != null)))
                                thirdLevel.Add(new XAttribute("sharing-policy", "terminal-wide"));
                            thirdLevel.Add(new XAttribute("purchase_date", licenceInfo.licences[n - 1].EntryConstant.PurchaseDate.ToString("yyyy.MM.dd")));
                            thirdLevel.Add(new XAttribute("support_expiration_date", licenceInfo.licences[n - 1].EntryConstant.SupportExpirationDate.ToString("yyyy.MM.dd")));
                            thirdLevel.Add(new XAttribute("guarantee_expiration_date", licenceInfo.licences[n - 1].EntryConstant.GuaranteeExpirationDate.ToString("yyyy.MM.dd")));
                            secondLevel.Add(thirdLevel);

                            licenceConstantAmount += licenceInfo.licences[n - 1].EntryConstant.Licenсes;
                        }
                        if (licenceConstantAmount != 0)
                            resultText = resultText + licenceInfo.licences[n - 1].RusName + "=" + licenceConstantAmount.ToString() + "; ";
                    }
                }
            }
            if (nullLicBlock != null)
            {
                //удаление коммента
                XNode comment = nullLicBlock.PreviousNode;
                comment.Remove();
                //удаление пустого блока лицензий
                nullLicBlock.Remove();
                nullLicBlock = null;
            }
            #endregion

            #region обработка раздела "dongles_pack"

            firstLevel = newXml.Root.Element("dongles_pack");

            //установка пары город_ведомство
            firstLevel.Attribute("id").SetValue(cityTextbox.Text + "_" + departmentTextbox.Text);
            XNode position = firstLevel.FirstNode;
            //установка параметров ключей

            if (mainKeyCheckBox.Checked)
            {
                XComment mainKeyComment = (XComment)firstLevel.FirstNode;
                mainKeyComment.Value = mainKeyNumberTextbox.Text;
                XElement dongle = (XElement)mainKeyComment.NextNode;
                dongle.Attribute("id").SetValue(mainKeyIdTextbox.Text);
                dongle.Attribute("TRU_cypher").SetValue(mainKeyCodeTextbox.Text);
                dongle.Attribute("type").SetValue("master");
                position = dongle.NextNode;
            }
            else
            {
                XNode comment = firstLevel.FirstNode;
                comment.Remove();
                XNode dongle = firstLevel.FirstNode;
                dongle.Remove();
                position = firstLevel.FirstNode;
            }

            if (reserveKeyCheckBox.Checked)
            {
                XComment reserveKeyComment = (XComment)position;
                reserveKeyComment.Value = reserveKeyNumberTextbox.Text;
                XElement dongle = (XElement)reserveKeyComment.NextNode;
                dongle.Attribute("id").SetValue(reserveKeyIdTextbox.Text);
                dongle.Attribute("TRU_cypher").SetValue(reserveKeyCodeTextbox.Text);
                dongle.Attribute("type").SetValue("reserved");
            }
            else
            {
                position.NextNode.Remove();
                position.Remove();
            }

            #endregion

            #region обработка раздела "TRU_requests"
            if ((String.IsNullOrEmpty(mainKeyRequestTextbox.Text)) &&
                (String.IsNullOrEmpty(reserveKeyRequestTextbox.Text)))
            {
                newXml.Root.Element("TRU_requests").Remove();
                return 0;
            }
            else
            {
                firstLevel = newXml.Root.Element("TRU_requests");

                //установка параметров ключей
                if (mainKeyCheckBox.Checked)
                {
                    XComment mainKeyComment = (XComment)firstLevel.FirstNode;
                    mainKeyComment.Value = mainKeyNumberTextbox.Text;
                    XElement trueRequest = (XElement)mainKeyComment.NextNode;
                    trueRequest.Attribute("request").SetValue(mainKeyRequestTextbox.Text);
                    position = trueRequest.NextNode;
                }
                else
                {
                    XNode comment = firstLevel.FirstNode;
                    comment.Remove();
                    XNode trueRequest = firstLevel.FirstNode;
                    trueRequest.Remove();
                    position = firstLevel.FirstNode;
                }

                if (reserveKeyCheckBox.Checked)
                {
                    XComment reserveKeyComment = (XComment)position;
                    reserveKeyComment.Value = reserveKeyNumberTextbox.Text;
                    XElement trueRequest = (XElement)reserveKeyComment.NextNode;
                    trueRequest.Attribute("request").SetValue(reserveKeyRequestTextbox.Text);
                    position = trueRequest.NextNode;
                }
                else
                {
                    position.NextNode.Remove();
                    position.Remove();
                }
                return 1;
            }
            #endregion
        }


        //кнопка "Cохранить"
        private void maintoolSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataParser();

                if (null == newXml)
                {
                    saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Файлы XML (*.xml)|*.xml";
                    if ((mainKeyCheckBox.Checked) && (reserveKeyCheckBox.Checked))
                    {
                        saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                            departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                            "_" + reserveKeyNumberTextbox.Text + "]-[" +
                            dateBox.Value.ToString("dd.MM.yyyy") + "]";
                    }
                    if ((mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
                    {
                        saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                            departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                            "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
                    }
                    if ((!mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
                    {
                        saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                            departmentTextbox.Text + "_" + reserveKeyNumberTextbox.Text +
                            "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
                    }
                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    newXmlName = saveFileDialog.FileName;
                }
                newXml = new XDocument(etalonXml);
                newXmlFilling();

                //сохранение файла
                newXml.Save(newXmlName, SaveOptions.None);
                int i = newXmlName.LastIndexOf('\\');
                this.Text = MAIN_TITLE + newXmlName.Substring(i + 1);

                //открытие окна с результатом
                ResultForm resultForm = new ResultForm(resultText);
                resultForm.Text = RESULT_FORM_TITLE;
                resultForm.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, ERROR_TITLE, MessageBoxButtons.OK);
            }
        }

        //кнопка "Сохранить как..."
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataParser();

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы XML (*.xml)|*.xml";
            if ((mainKeyCheckBox.Checked) && (reserveKeyCheckBox.Checked))
            {
                saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                    "_" + reserveKeyNumberTextbox.Text + "]-[" +
                    dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }
            if ((mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
            {
                saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                    "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }
            if ((!mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
            {
                saveFileDialog.FileName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + reserveKeyNumberTextbox.Text +
                    "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            newXml = new XDocument(etalonXml);
            newXmlName = saveFileDialog.FileName;

            newXmlFilling();

            //сохранение файла
            newXml.Save(newXmlName, SaveOptions.None);
            int i = newXmlName.LastIndexOf('\\');
            this.Text = MAIN_TITLE + newXmlName.Substring(i + 1);

            //открытие окна с результатом
            ResultForm resultForm = new ResultForm(resultText);
            resultForm.Text = RESULT_FORM_TITLE;
            resultForm.Show();
        }

        #endregion

        #region ГЕНЕРАЦИЯ КОНТЕЙНЕРА
        //кнопка "Генерировать"
        private void maintoolGenerate_Click(object sender, EventArgs e)
        {
            #region сохранение файла
            
            DataParser();

            if ((mainKeyCheckBox.Checked) && (reserveKeyCheckBox.Checked))
            {
                newXmlName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                    "_" + reserveKeyNumberTextbox.Text + "]-[" +
                    dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }
            if ((mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
            {
                newXmlName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + mainKeyNumberTextbox.Text +
                    "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }
            if ((!mainKeyCheckBox.Checked) && (reserveKeyCheckBox.Checked))
            {
                newXmlName = "[" + cityTextbox.Text + "_" +
                    departmentTextbox.Text + "_" + reserveKeyNumberTextbox.Text +
                    "]-[" + dateBox.Value.ToString("dd.MM.yyyy") + "]";
            }

            newXml = new XDocument(etalonXml);
            int isUpdate = newXmlFilling();

            //сохранение файла
            String newXmlNameBin = "";
            if (0 == isUpdate)
                newXmlNameBin = newXmlName + ".licences.bin";
            if (1 == isUpdate)
                newXmlNameBin = newXmlName + ".container.bin";

            newXmlName = newXmlName + ".xml";
            newXml.Save(newXmlName, SaveOptions.None);
            mainForm.ActiveForm.Text = MAIN_TITLE + newXmlName;

            #endregion

            String cmd = newXmlName + " " + newXmlNameBin;

            Process newProc = Process.Start(LPMAKER_NAME, cmd);
            newProc.WaitForExit();
            newProc.Close();

            //открытие окна с результатом
            ResultForm resultForm = new ResultForm(resultText);
            resultForm.Text = RESULT_FORM_TITLE;
            resultForm.Show();
        }
        #endregion

        #region ИНИЦИАЛИЗАЦИЯ КЛЮЧЕЙ
        //кнопка "Инициализировать ключи"
        private void maintoolInitialize_Click(object sender, EventArgs e)
        {
            #region инициализация ключей
            if (mainKeyCheckBox.Checked)
            {
                MessageBox.Show(INSERT_MAIN_TITLE);

                String parameters = "init " + MAIN_KEY_TEMP_INIT_FILE_NAME;
                Process newProc = Process.Start(GDSTOOL_NAME, parameters);
                newProc.WaitForExit();
                newProc.Close();
            }
            if (reserveKeyCheckBox.Checked)
            {
                MessageBox.Show(INSERT_RESERVE_TITLE);

                String parameters = "init " + RESERVE_KEY_TEMP_INIT_FILE_NAME;
                Process newProc = Process.Start(GDSTOOL_NAME, parameters);
                newProc.WaitForExit();
                newProc.Close();
            }
            #endregion


            int posStart = 0, posEnd = 0;

            #region считывание инфы из временных файлов и подставновка в поля формы

            if (mainKeyCheckBox.Checked)
            {
                FileStream mainKeyTempInitFile = new FileStream(MAIN_KEY_TEMP_INIT_FILE_NAME, FileMode.Open);
                StreamReader mainKeyTempInitFileReader = new StreamReader(mainKeyTempInitFile);
                String mainKeyId = "";
                String mainKeyCode = "";
                mainKeyId = mainKeyTempInitFileReader.ReadLine();
                posStart = mainKeyId.IndexOf('\"');
                posEnd = mainKeyId.LastIndexOf('\"');
                mainKeyId = mainKeyId.Substring(posStart + 1, posEnd - posStart - 1);

                mainKeyCode = mainKeyTempInitFileReader.ReadLine();
                posStart = mainKeyCode.IndexOf('\"');
                posEnd = mainKeyCode.LastIndexOf('\"');
                mainKeyCode = mainKeyCode.Substring(posStart + 1, posEnd - posStart - 1);

                mainKeyIdTextbox.Text = mainKeyId;
                mainKeyCodeTextbox.Text = mainKeyCode;

            }
            if (reserveKeyCheckBox.Checked)
            {
                FileStream reserveKeyTempInitFile = new FileStream(RESERVE_KEY_TEMP_INIT_FILE_NAME, FileMode.Open);
                StreamReader reserveKeyTempInitFileReader = new StreamReader(reserveKeyTempInitFile);
                String reserveKeyId = "";
                String reserveKeyCode = "";
                reserveKeyId = reserveKeyTempInitFileReader.ReadLine();
                posStart = reserveKeyId.IndexOf('\"');
                posEnd = reserveKeyId.LastIndexOf('\"');
                reserveKeyId = reserveKeyId.Substring(posStart + 1, posEnd - posStart - 1);

                reserveKeyCode = reserveKeyTempInitFileReader.ReadLine();
                posStart = reserveKeyCode.IndexOf('\"');
                posEnd = reserveKeyCode.LastIndexOf('\"');
                reserveKeyCode = reserveKeyCode.Substring(posStart + 1, posEnd - posStart - 1);

                reserveKeyIdTextbox.Text = reserveKeyId;
                reserveKeyCodeTextbox.Text = reserveKeyCode;
            }

            #endregion

        }

        //формирование файла readme.xml
        private void maintoolCreateReadme_Click(object sender, EventArgs e)
        {
            try
            {
                #region создание файла
                readmeXml = new XDocument();
                XmlWriter readmeXmlWriter = readmeXml.CreateWriter();

                readmeXmlWriter.WriteStartDocument();
                readmeXmlWriter.WriteStartElement("keys");

                readmeXmlWriter.WriteStartAttribute("city");
                readmeXmlWriter.WriteValue(cityTextbox.Text);
                readmeXmlWriter.WriteEndAttribute();

                readmeXmlWriter.WriteStartAttribute("department");
                readmeXmlWriter.WriteValue(departmentTextbox.Text);
                readmeXmlWriter.WriteEndAttribute();

                readmeXmlWriter.WriteStartAttribute("date");
                readmeXmlWriter.WriteValue(dateBox.Value.ToString("dd.MM.yyyy"));
                readmeXmlWriter.WriteEndAttribute();
                #endregion

                #region проверка поля с названием города
                if (String.IsNullOrEmpty(cityTextbox.Text))
                    throw new Exception(ERROR_UNKNOWN_CITY);
                #endregion

                #region проверка поля с названием ведомства
                if (String.IsNullOrEmpty(departmentTextbox.Text))
                    throw new Exception(ERROR_UNKNOWN_DEPARTMENT);
                #endregion

                #region заполнение xml-файла
                if (mainKeyCheckBox.Checked)
                {
                    if (String.IsNullOrEmpty(mainKeyNumberTextbox.Text))
                        throw new Exception(ERROR_UNKNOWN_MAIN_KEY_NUM);

                    readmeXmlWriter.WriteStartElement("keyMaster");

                    readmeXmlWriter.WriteStartAttribute("number");
                    readmeXmlWriter.WriteValue(mainKeyNumberTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("id");
                    readmeXmlWriter.WriteValue(mainKeyIdTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("TRU_cypher");
                    readmeXmlWriter.WriteValue(mainKeyCodeTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("type");
                    readmeXmlWriter.WriteValue("master");
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteEndElement();
                }
                if (reserveKeyCheckBox.Checked)
                {
                    if (String.IsNullOrEmpty(reserveKeyNumberTextbox.Text))
                        throw new Exception(ERROR_UNKNOWN_RESERVE_KEY_NUM);

                    readmeXmlWriter.WriteStartElement("keyReserved");

                    readmeXmlWriter.WriteStartAttribute("number");
                    readmeXmlWriter.WriteValue(reserveKeyNumberTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("id");
                    readmeXmlWriter.WriteValue(reserveKeyIdTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("TRU_cypher");
                    readmeXmlWriter.WriteValue(reserveKeyCodeTextbox.Text);
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteStartAttribute("type");
                    readmeXmlWriter.WriteValue("reserved");
                    readmeXmlWriter.WriteEndAttribute();

                    readmeXmlWriter.WriteEndElement();
                }

                if ((!mainKeyCheckBox.Checked) && (!reserveKeyCheckBox.Checked))
                    throw new Exception(ERROR_NO_DATA_KEYS);

                readmeXmlWriter.WriteEndElement();
                readmeXmlWriter.WriteEndDocument();
                readmeXmlWriter.Close();

                #endregion

                //сохранение файла readme.xml
                String readmeXmlName = "";
                if ((mainKeyCheckBox.Checked) && (reserveKeyCheckBox.Checked))
                    readmeXmlName = "readme_" + mainKeyNumberTextbox.Text + "_" + reserveKeyNumberTextbox.Text + ".xml";
                else
                {
                    if (mainKeyCheckBox.Checked)
                        readmeXmlName = "readme_" + mainKeyNumberTextbox.Text + ".xml";
                    if (reserveKeyCheckBox.Checked)
                        readmeXmlName = "readme_" + reserveKeyNumberTextbox.Text + ".xml";
                }
                readmeXml.Save(readmeXmlName, SaveOptions.None);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, ERROR_TITLE, MessageBoxButtons.OK);
            }
        }

        //загрузить из файла readme.xml в форму информацию о ключах
        private void maintoolLoadKeyInfo_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы XML (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            String readmeXmlName = openFileDialog.FileName;
            readmeXml = XDocument.Load(readmeXmlName);

            cityTextbox.Text = readmeXml.Root.Attribute("city").Value;
            departmentTextbox.Text = readmeXml.Root.Attribute("department").Value;
            dateBox.Text = readmeXml.Root.Attribute("date").Value.ToString();

            XElement firstLevel = readmeXml.Root.Element("keyMaster");
            if (firstLevel != null)
            {
                mainKeyNumberTextbox.Text = firstLevel.Attribute("number").Value;
                mainKeyIdTextbox.Text = firstLevel.Attribute("id").Value;
                mainKeyCodeTextbox.Text = firstLevel.Attribute("TRU_cypher").Value;
            }

            firstLevel = readmeXml.Root.Element("keyReserved");
            if (firstLevel != null)
            {
                reserveKeyNumberTextbox.Text = firstLevel.Attribute("number").Value;
                reserveKeyIdTextbox.Text = firstLevel.Attribute("id").Value;
                reserveKeyCodeTextbox.Text = firstLevel.Attribute("TRU_cypher").Value;
            }

        }
        #endregion

        #region КОНТРОЛЛЕРЫ
        #region управление количеством ключей
        private void mainKeyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainKeyNumberTextbox.Enabled = !mainKeyNumberTextbox.Enabled;
            mainKeyIdTextbox.Enabled = !mainKeyIdTextbox.Enabled;
            mainKeyCodeTextbox.Enabled = !mainKeyCodeTextbox.Enabled;
            mainKeyRequestTextbox.Enabled = !mainKeyRequestTextbox.Enabled;
        }

        private void reserveKeyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            reserveKeyNumberTextbox.Enabled = !reserveKeyNumberTextbox.Enabled;
            reserveKeyIdTextbox.Enabled = !reserveKeyIdTextbox.Enabled;
            reserveKeyCodeTextbox.Enabled = !reserveKeyCodeTextbox.Enabled;
            reserveKeyRequestTextbox.Enabled = !reserveKeyRequestTextbox.Enabled;
        }
        #endregion

        #region управление активностью полей выбора даты временных лицензий

        //1
        private void magreadTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if (sameMagreadCheckBox.Checked)
            {
                audioplayerTempAmountTextbox.Text = magreadTempAmountTextbox.Text;
            }

            if ((!String.IsNullOrEmpty(magreadTempAmountTextbox.Text)) && (int.Parse(magreadTempAmountTextbox.Text) > 0))
                magreadTempDatebox.Enabled = true;
            else
                magreadTempDatebox.Enabled = false;
        }

        //2
        private void taskjournalTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(taskjournalTempAmountTextbox.Text)) && (int.Parse(taskjournalTempAmountTextbox.Text) > 0))
                taskjournalTempDatebox.Enabled = true;
            else
                taskjournalTempDatebox.Enabled = false;
        }

        //3
        private void magfaxTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magfaxTempAmountTextbox.Text)) && (int.Parse(magfaxTempAmountTextbox.Text) > 0))
                magfaxTempDatebox.Enabled = true;
            else
                magfaxTempDatebox.Enabled = false;
        }

        //4
        private void trackingTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trackingTempAmountTextbox.Text)) && (int.Parse(trackingTempAmountTextbox.Text) > 0))
                trackingTempDatebox.Enabled = true;
            else
                trackingTempDatebox.Enabled = false;
        }

        //5
        private void audioplayerTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(audioplayerTempAmountTextbox.Text)) && (int.Parse(audioplayerTempAmountTextbox.Text) > 0) && (!sameMagreadCheckBox.Checked))
                audioplayerTempDatebox.Enabled = true;
            else
                audioplayerTempDatebox.Enabled = false;
        }

        //6
        private void billclientTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(billclientTempAmountTextbox.Text)) && (int.Parse(billclientTempAmountTextbox.Text) > 0))
                billclientTempDatebox.Enabled = true;
            else
                billclientTempDatebox.Enabled = false;
        }

        //7
        private void mwieserverTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(mwieserverTempAmountTextbox.Text)) && (int.Parse(mwieserverTempAmountTextbox.Text) > 0))
                mwieserverTempDatebox.Enabled = true;
            else
                mwieserverTempDatebox.Enabled = false;
        }

        //8
        private void mwieclientTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(mwieclientTempAmountTextbox.Text)) && (int.Parse(mwieclientTempAmountTextbox.Text) > 0))
                mwieclientTempDatebox.Enabled = true;
            else
                mwieclientTempDatebox.Enabled = false;
        }

        //9
        private void mwiestationTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(mwiestationTempAmountTextbox.Text)) && (int.Parse(mwiestationTempAmountTextbox.Text) > 0))
                mwiestationTempDatebox.Enabled = true;
            else
                mwiestationTempDatebox.Enabled = false;
        }

        //10
        private void integrationserverTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(integrationserverTempAmountTextbox.Text)) && (int.Parse(integrationserverTempAmountTextbox.Text) > 0))
                integrationserverTempDatebox.Enabled = true;
            else
                integrationserverTempDatebox.Enabled = false;
        }

        //11
        private void integrationadapterTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(integrationadapterTempAmountTextbox.Text)) && (int.Parse(integrationadapterTempAmountTextbox.Text) > 0))
                integrationadapterTempDatebox.Enabled = true;
            else
                integrationadapterTempDatebox.Enabled = false;
        }

        //12
        private void videojournalTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(videojournalTempAmountTextbox.Text)) && (int.Parse(videojournalTempAmountTextbox.Text) > 0))
                videojournalTempDatebox.Enabled = true;
            else
                videojournalTempDatebox.Enabled = false;
        }

        //13
        private void imitatorsormTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(imitatorsormTempAmountTextbox.Text)) && (int.Parse(imitatorsormTempAmountTextbox.Text) > 0))
                imitatorsormTempDatebox.Enabled = true;
            else
                imitatorsormTempDatebox.Enabled = false;
        }

        //14
        private void maggradientTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(maggradientTempAmountTextbox.Text)) && (int.Parse(maggradientTempAmountTextbox.Text) > 0))
                maggradientTempDatebox.Enabled = true;
            else
                maggradientTempDatebox.Enabled = false;
        }

        //15
        private void tracking3TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(tracking3TempAmountTextbox.Text)) && (int.Parse(tracking3TempAmountTextbox.Text) > 0))
                tracking3TempDatebox.Enabled = true;
            else
                tracking3TempDatebox.Enabled = false;
        }

        //16
        private void gatemegafonTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatemegafonTempAmountTextbox.Text)) && (int.Parse(gatemegafonTempAmountTextbox.Text) > 0))
                gatemegafonTempDatebox.Enabled = true;
            else
                gatemegafonTempDatebox.Enabled = false;
        }

        //17
        private void gatemtsTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatemtsTempAmountTextbox.Text)) && (int.Parse(gatemtsTempAmountTextbox.Text) > 0))
                gatemtsTempDatebox.Enabled = true;
            else
                gatemtsTempDatebox.Enabled = false;
        }

        //18
        private void gatebeelineTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatebeelineTempAmountTextbox.Text)) && (int.Parse(gatebeelineTempAmountTextbox.Text) > 0))
                gatebeelineTempDatebox.Enabled = true;
            else
                gatebeelineTempDatebox.Enabled = false;
        }

        //19
        private void istokclientTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(istokclientTempAmountTextbox.Text)) && (int.Parse(istokclientTempAmountTextbox.Text) > 0))
                istokclientTempDatebox.Enabled = true;
            else
                istokclientTempDatebox.Enabled = false;
        }

        //20
        private void higinaconverterTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(higinaconverterTempAmountTextbox.Text)) && (int.Parse(higinaconverterTempAmountTextbox.Text) > 0))
                higinaconverterTempDatebox.Enabled = true;
            else
                higinaconverterTempDatebox.Enabled = false;
        }

        //21
        private void webmapsTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(webmapsTempAmountTextbox.Text)) && (int.Parse(webmapsTempAmountTextbox.Text) > 0))
                webmapsTempDatebox.Enabled = true;
            else
                webmapsTempDatebox.Enabled = false;
        }

        //22
        private void gateYanvarTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateyanvarTempAmountTextbox.Text)) && (int.Parse(gateyanvarTempAmountTextbox.Text) > 0))
                gateyanvarTempDatebox.Enabled = true;
            else
                gateyanvarTempDatebox.Enabled = false;
        }

        //23
        private void play3gvideoTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(play3gvideoTempAmountTextbox.Text)) && (int.Parse(play3gvideoTempAmountTextbox.Text) > 0))
                play3gvideoTempDatebox.Enabled = true;
            else
                play3gvideoTempDatebox.Enabled = false;
        }

        //24
        private void integrationnetconfigTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(integrationnetconfigTempAmountTextbox.Text)) && (int.Parse(integrationnetconfigTempAmountTextbox.Text) > 0))
                integrationnetconfigTempDatebox.Enabled = true;
            else
                integrationnetconfigTempDatebox.Enabled = false;
        }

        //25
        private void trackingg2ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trackingg2ConstantAmountTextbox.Text)) && (int.Parse(trackingg2ConstantAmountTextbox.Text) > 0))
            {
                trackingg2ConstantHelpdeskEndDatebox.Enabled = true;
                trackingg2ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                trackingg2ConstantHelpdeskEndDatebox.Enabled = false;
                trackingg2ConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void trackingg2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trackingg2TempAmountTextbox.Text)) && (int.Parse(trackingg2TempAmountTextbox.Text) > 0))
            {
                trackingg2TempDatebox.Enabled = true;
                trackingg2TempHelpdeskEndDatebox.Enabled = true;
                trackingg2TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                trackingg2TempDatebox.Enabled = false;
                trackingg2TempHelpdeskEndDatebox.Enabled = false;
                trackingg2TempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //26
        private void webmapsg2ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(webmapsg2ConstantAmountTextbox.Text)) && (int.Parse(webmapsg2ConstantAmountTextbox.Text) > 0))
            {
                webmapsg2ConstantHelpdeskEndDatebox.Enabled = true;
                webmapsg2ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                webmapsg2ConstantHelpdeskEndDatebox.Enabled = false;
                webmapsg2ConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void webmapsg2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(webmapsg2TempAmountTextbox.Text)) && (int.Parse(webmapsg2TempAmountTextbox.Text) > 0))
            {
                webmapsg2TempDatebox.Enabled = true;
                webmapsg2TempHelpdeskEndDatebox.Enabled = true;
                webmapsg2TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                webmapsg2TempDatebox.Enabled = false;
                webmapsg2TempHelpdeskEndDatebox.Enabled = false;
                webmapsg2TempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //27
        private void magreadg2ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magreadg2ConstantAmountTextbox.Text)) && (int.Parse(magreadg2ConstantAmountTextbox.Text) > 0))
            {
                magreadg2ConstantHelpdeskEndDatebox.Enabled = true;
                magreadg2ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magreadg2ConstantHelpdeskEndDatebox.Enabled = false;
                magreadg2ConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void magreadg2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magreadg2TempAmountTextbox.Text)) && (int.Parse(magreadg2TempAmountTextbox.Text) > 0))
            {
                magreadg2TempDatebox.Enabled = true;
                magreadg2TempHelpdeskEndDatebox.Enabled = true;
                magreadg2TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magreadg2TempDatebox.Enabled = false;
                magreadg2TempHelpdeskEndDatebox.Enabled = false;
                magreadg2TempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //28
        private void videojournalg2ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(videojournalg2ConstantAmountTextbox.Text)) && (int.Parse(videojournalg2ConstantAmountTextbox.Text) > 0))
            {
                videojournalg2ConstantHelpdeskEndDatebox.Enabled = true;
                videojournalg2ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                videojournalg2ConstantHelpdeskEndDatebox.Enabled = false;
                videojournalg2ConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void videojournalg2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(videojournalg2TempAmountTextbox.Text)) && (int.Parse(videojournalg2TempAmountTextbox.Text) > 0))
            {
                videojournalg2TempDatebox.Enabled = true;
                videojournalg2TempHelpdeskEndDatebox.Enabled = true;
                videojournalg2TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                videojournalg2TempDatebox.Enabled = false;
                videojournalg2TempHelpdeskEndDatebox.Enabled = false;
                videojournalg2TempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //29
        private void gateutkTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateutkTempAmountTextbox.Text)) && (int.Parse(gateutkTempAmountTextbox.Text) > 0))
                gateutkTempDatebox.Enabled = true;
            else
                gateutkTempDatebox.Enabled = false;
        }

        //30
        private void smsserviceConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(smsserviceConstantAmountTextbox.Text)) && (int.Parse(smsserviceConstantAmountTextbox.Text) > 0))
            {
                smsserviceConstantHelpdeskEndDatebox.Enabled = true;
                smsserviceConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                smsserviceConstantHelpdeskEndDatebox.Enabled = false;
                smsserviceConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void smsserviceTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(smsserviceTempAmountTextbox.Text)) && (int.Parse(smsserviceTempAmountTextbox.Text) > 0))
            {
                smsserviceTempDatebox.Enabled = true;
                smsserviceTempHelpdeskEndDatebox.Enabled = true;
                smsserviceTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                smsserviceTempDatebox.Enabled = false;
                smsserviceTempHelpdeskEndDatebox.Enabled = false;
                smsserviceTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //31
        private void simserviceConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(simserviceConstantAmountTextbox.Text)) && (int.Parse(simserviceConstantAmountTextbox.Text) > 0))
            {
                simserviceConstantHelpdeskEndDatebox.Enabled = true;
                simserviceConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                simserviceConstantHelpdeskEndDatebox.Enabled = false;
                simserviceConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void simserviceTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(simserviceTempAmountTextbox.Text)) && (int.Parse(simserviceTempAmountTextbox.Text) > 0))
            {
                simserviceTempDatebox.Enabled = true;
                simserviceTempHelpdeskEndDatebox.Enabled = true;
                simserviceTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                simserviceTempDatebox.Enabled = false;
                simserviceTempHelpdeskEndDatebox.Enabled = false;
                simserviceTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //32
        private void barcodesadminConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodesadminConstantAmountTextbox.Text)) && (int.Parse(barcodesadminConstantAmountTextbox.Text) > 0))
            {
                barcodesadminConstantHelpdeskEndDatebox.Enabled = true;
                barcodesadminConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodesadminConstantHelpdeskEndDatebox.Enabled = false;
                barcodesadminConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void barcodesadminTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodesadminTempAmountTextbox.Text)) && (int.Parse(barcodesadminTempAmountTextbox.Text) > 0))
            {
                barcodesadminTempDatebox.Enabled = true;
                barcodesadminTempHelpdeskEndDatebox.Enabled = true;
                barcodesadminTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodesadminTempDatebox.Enabled = false;
                barcodesadminTempHelpdeskEndDatebox.Enabled = false;
                barcodesadminTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //33
        private void gatesmartsTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatesmartsTempAmountTextbox.Text)) && (int.Parse(gatesmartsTempAmountTextbox.Text) > 0))
                gatesmartsTempDatebox.Enabled = true;
            else
                gatesmartsTempDatebox.Enabled = false;
        }

        //34
        private void simmagplaceserviceConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(simmagplaceserviceConstantAmountTextbox.Text)) && (int.Parse(simmagplaceserviceConstantAmountTextbox.Text) > 0))
            {
                simmagplaceserviceConstantHelpdeskEndDatebox.Enabled = true;
                simmagplaceserviceConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                simmagplaceserviceConstantHelpdeskEndDatebox.Enabled = false;
                simmagplaceserviceConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void simmagplaceserviceTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(simmagplaceserviceTempAmountTextbox.Text)) && (int.Parse(simmagplaceserviceTempAmountTextbox.Text) > 0))
            {
                simmagplaceserviceTempDatebox.Enabled = true;
                simmagplaceserviceTempHelpdeskEndDatebox.Enabled = true;
                simmagplaceserviceTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                simmagplaceserviceTempDatebox.Enabled = false;
                simmagplaceserviceTempHelpdeskEndDatebox.Enabled = false;
                simmagplaceserviceTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //35
        private void armnvdConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(armnvdConstantAmountTextbox.Text)) && (int.Parse(armnvdConstantAmountTextbox.Text) > 0))
            {
                armnvdConstantHelpdeskEndDatebox.Enabled = true;
                armnvdConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                armnvdConstantHelpdeskEndDatebox.Enabled = false;
                armnvdConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void armnvdTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(armnvdTempAmountTextbox.Text)) && (int.Parse(armnvdTempAmountTextbox.Text) > 0))
            {
                armnvdTempDatebox.Enabled = true;
                armnvdTempHelpdeskEndDatebox.Enabled = true;
                armnvdTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                armnvdTempDatebox.Enabled = false;
                armnvdTempHelpdeskEndDatebox.Enabled = false;
                armnvdTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //36
        private void devicecontrolserverConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(devicecontrolserverConstantAmountTextbox.Text)) && (int.Parse(devicecontrolserverConstantAmountTextbox.Text) > 0))
            {
                devicecontrolserverConstantHelpdeskEndDatebox.Enabled = true;
                devicecontrolserverConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                devicecontrolserverConstantHelpdeskEndDatebox.Enabled = false;
                devicecontrolserverConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void devicecontrolserverTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(devicecontrolserverTempAmountTextbox.Text)) && (int.Parse(devicecontrolserverTempAmountTextbox.Text) > 0))
            {
                devicecontrolserverTempDatebox.Enabled = true;
                devicecontrolserverTempHelpdeskEndDatebox.Enabled = true;
                devicecontrolserverTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                devicecontrolserverTempDatebox.Enabled = false;
                devicecontrolserverTempHelpdeskEndDatebox.Enabled = false;
                devicecontrolserverTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //37
        private void trassatestTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trassatestTempAmountTextbox.Text)) && (int.Parse(trassatestTempAmountTextbox.Text) > 0))
                trassatestTempDatebox.Enabled = true;
            else
                trassatestTempDatebox.Enabled = false;
        }

        //38
        private void trassavkpruTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trassavkpruTempAmountTextbox.Text)) && (int.Parse(trassavkpruTempAmountTextbox.Text) > 0))
                trassavkpruTempDatebox.Enabled = true;
            else
                trassavkpruTempDatebox.Enabled = false;
        }

        //39
        private void trassavkputcpipTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trassavkputcpipTempAmountTextbox.Text)) && (int.Parse(trassavkputcpipTempAmountTextbox.Text) > 0))
                trassavkputcpipTempDatebox.Enabled = true;
            else
                trassavkputcpipTempDatebox.Enabled = false;
        }

        //40
        private void trassavkpputcpipTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(trassavkpputcpipTempAmountTextbox.Text)) && (int.Parse(trassavkpputcpipTempAmountTextbox.Text) > 0))
                trassavkpputcpipTempDatebox.Enabled = true;
            else
                trassavkpputcpipTempDatebox.Enabled = false;
        }

        //41
        private void gateservicevkTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateservicevkTempAmountTextbox.Text)) && (int.Parse(gateservicevkTempAmountTextbox.Text) > 0))
                gateservicevkTempDatebox.Enabled = true;
            else
                gateservicevkTempDatebox.Enabled = false;
        }

        //42
        private void avk4TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(avk4TempAmountTextbox.Text)) && (int.Parse(avk4TempAmountTextbox.Text) > 0))
                avk4TempDatebox.Enabled = true;
            else
                avk4TempDatebox.Enabled = false;
        }

        //43
        private void magbiyaconnectionConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magbiyaconnectionConstantAmountTextbox.Text)) && (int.Parse(magbiyaconnectionConstantAmountTextbox.Text) > 0))
            {
                magbiyaconnectionConstantHelpdeskEndDatebox.Enabled = true;
                magbiyaconnectionConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magbiyaconnectionConstantHelpdeskEndDatebox.Enabled = false;
                magbiyaconnectionConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void magbiyaconnectionTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magbiyaconnectionTempAmountTextbox.Text)) && (int.Parse(magbiyaconnectionTempAmountTextbox.Text) > 0))
            {
                magbiyaconnectionTempDatebox.Enabled = true;
                magbiyaconnectionTempHelpdeskEndDatebox.Enabled = true;
                magbiyaconnectionTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magbiyaconnectionTempDatebox.Enabled = false;
                magbiyaconnectionTempHelpdeskEndDatebox.Enabled = false;
                magbiyaconnectionTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //44
        private void voiceidentificationConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(voiceidentificationConstantAmountTextbox.Text)) && (int.Parse(voiceidentificationConstantAmountTextbox.Text) > 0))
            {
                voiceidentificationConstantHelpdeskEndDatebox.Enabled = true;
                voiceidentificationConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                voiceidentificationConstantHelpdeskEndDatebox.Enabled = false;
                voiceidentificationConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void voiceidentificationTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(voiceidentificationTempAmountTextbox.Text)) && (int.Parse(voiceidentificationTempAmountTextbox.Text) > 0))
            {
                voiceidentificationTempDatebox.Enabled = true;
                voiceidentificationTempHelpdeskEndDatebox.Enabled = true;
                voiceidentificationTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                voiceidentificationTempDatebox.Enabled = false;
                voiceidentificationTempHelpdeskEndDatebox.Enabled = false;
                voiceidentificationTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //45
        private void mobileobjectstrackingConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(mobileobjectstrackingConstantAmountTextbox.Text)) && (int.Parse(mobileobjectstrackingConstantAmountTextbox.Text) > 0))
            {
                mobileobjectstrackingConstantHelpdeskEndDatebox.Enabled = true;
                mobileobjectstrackingConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                mobileobjectstrackingConstantHelpdeskEndDatebox.Enabled = false;
                mobileobjectstrackingConstantGuaranteeEndDatebox.Enabled = false;
            }

        }

        private void mobileobjectstrackingTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(mobileobjectstrackingTempAmountTextbox.Text)) && (int.Parse(mobileobjectstrackingTempAmountTextbox.Text) > 0))
            {
                mobileobjectstrackingTempDatebox.Enabled = true;
                mobileobjectstrackingTempHelpdeskEndDatebox.Enabled = true;
                mobileobjectstrackingTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                mobileobjectstrackingTempDatebox.Enabled = false;
                mobileobjectstrackingTempHelpdeskEndDatebox.Enabled = false;
                mobileobjectstrackingTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //46
        private void barcodek2pConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2pConstantAmountTextbox.Text)) && (int.Parse(barcodek2pConstantAmountTextbox.Text) > 0))
            {
                barcodek2pConstantHelpdeskEndDatebox.Enabled = true;
                barcodek2pConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2pConstantHelpdeskEndDatebox.Enabled = false;
                barcodek2pConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void barcodek2pTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2pTempAmountTextbox.Text)) && (int.Parse(barcodek2pTempAmountTextbox.Text) > 0))
            {
                barcodek2pTempDatebox.Enabled = true;
                barcodek2pTempHelpdeskEndDatebox.Enabled = true;
                barcodek2pTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2pTempDatebox.Enabled = false;
                barcodek2pTempHelpdeskEndDatebox.Enabled = false;
                barcodek2pTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //47
        private void barcodek2rConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2rConstantAmountTextbox.Text)) && (int.Parse(barcodek2rConstantAmountTextbox.Text) > 0))
            {
                barcodek2rConstantHelpdeskEndDatebox.Enabled = true;
                barcodek2rConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2rConstantHelpdeskEndDatebox.Enabled = false;
                barcodek2rConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void barcodek2rTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2rTempAmountTextbox.Text)) && (int.Parse(barcodek2rTempAmountTextbox.Text) > 0))
            {
                barcodek2rTempDatebox.Enabled = true;
                barcodek2rTempHelpdeskEndDatebox.Enabled = true;
                barcodek2rTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2rTempDatebox.Enabled = false;
                barcodek2rTempHelpdeskEndDatebox.Enabled = false;
                barcodek2rTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //48
        private void barcodek2fConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2fConstantAmountTextbox.Text)) && (int.Parse(barcodek2fConstantAmountTextbox.Text) > 0))
            {
                barcodek2fConstantHelpdeskEndDatebox.Enabled = true;
                barcodek2fConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2fConstantHelpdeskEndDatebox.Enabled = false;
                barcodek2fConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void barcodek2fTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(barcodek2fTempAmountTextbox.Text)) && (int.Parse(barcodek2fTempAmountTextbox.Text) > 0))
            {
                barcodek2fTempDatebox.Enabled = true;
                barcodek2fTempHelpdeskEndDatebox.Enabled = true;
                barcodek2fTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                barcodek2fTempDatebox.Enabled = false;
                barcodek2fTempHelpdeskEndDatebox.Enabled = false;
                barcodek2fTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //49
        private void accountingbarcodeprintingConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(accountingbarcodeprintingConstantAmountTextbox.Text)) && (int.Parse(accountingbarcodeprintingConstantAmountTextbox.Text) > 0))
            {
                accountingbarcodeprintingConstantHelpdeskEndDatebox.Enabled = true;
                accountingbarcodeprintingConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                accountingbarcodeprintingConstantHelpdeskEndDatebox.Enabled = false;
                accountingbarcodeprintingConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void accountingbarcodeprintingTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(accountingbarcodeprintingTempAmountTextbox.Text)) && (int.Parse(accountingbarcodeprintingTempAmountTextbox.Text) > 0))
            {
                accountingbarcodeprintingTempDatebox.Enabled = true;
                accountingbarcodeprintingTempHelpdeskEndDatebox.Enabled = true;
                accountingbarcodeprintingTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                accountingbarcodeprintingTempDatebox.Enabled = false;
                accountingbarcodeprintingTempHelpdeskEndDatebox.Enabled = false;
                accountingbarcodeprintingTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //50
        private void analyzestatisticserviceConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(analyzestatisticserviceConstantAmountTextbox.Text)) && (int.Parse(analyzestatisticserviceConstantAmountTextbox.Text) > 0))
            {
                analyzestatisticserviceConstantHelpdeskEndDatebox.Enabled = true;
                analyzestatisticserviceConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                analyzestatisticserviceConstantHelpdeskEndDatebox.Enabled = false;
                analyzestatisticserviceConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void analyzestatisticserviceTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(analyzestatisticserviceTempAmountTextbox.Text)) && (int.Parse(analyzestatisticserviceTempAmountTextbox.Text) > 0))
            {
                analyzestatisticserviceTempDatebox.Enabled = true;
                analyzestatisticserviceTempHelpdeskEndDatebox.Enabled = true;
                analyzestatisticserviceTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                analyzestatisticserviceTempDatebox.Enabled = false;
                analyzestatisticserviceTempHelpdeskEndDatebox.Enabled = false;
                analyzestatisticserviceTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //51
        private void avk2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(avk2TempAmountTextbox.Text)) && (int.Parse(avk2TempAmountTextbox.Text) > 0))
                avk2TempDatebox.Enabled = true;
            else
                avk2TempDatebox.Enabled = false;
        }

        //52
        private void katunclientTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(katunclientTempAmountTextbox.Text)) && (int.Parse(katunclientTempAmountTextbox.Text) > 0))
                katunclientTempDatebox.Enabled = true;
            else
                katunclientTempDatebox.Enabled = false;
        }

        //53
        private void gatenssTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatenssTempAmountTextbox.Text)) && (int.Parse(gatenssTempAmountTextbox.Text) > 0))
                gatenssTempDatebox.Enabled = true;
            else
                gatenssTempDatebox.Enabled = false;
        }

        //54
        private void avk2uTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(avk2uTempAmountTextbox.Text)) && (int.Parse(avk2uTempAmountTextbox.Text) > 0))
                avk2uTempDatebox.Enabled = true;
            else
                avk2uTempDatebox.Enabled = false;
        }

        //55
        private void gateistokTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateistokTempAmountTextbox.Text)) && (int.Parse(gateistokTempAmountTextbox.Text) > 0))
                gateistokTempDatebox.Enabled = true;
            else
                gateistokTempDatebox.Enabled = false;
        }

        //56
        private void polyglotwebaccessConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(polyglotwebaccessConstantAmountTextbox.Text)) && (int.Parse(polyglotwebaccessConstantAmountTextbox.Text) > 0))
            {
                polyglotwebaccessConstantHelpdeskEndDatebox.Enabled = true;
                polyglotwebaccessConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                polyglotwebaccessConstantHelpdeskEndDatebox.Enabled = false;
                polyglotwebaccessConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void polyglotwebaccessTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(polyglotwebaccessTempAmountTextbox.Text)) && (int.Parse(polyglotwebaccessTempAmountTextbox.Text) > 0))
            {
                polyglotwebaccessTempDatebox.Enabled = true;
                polyglotwebaccessTempHelpdeskEndDatebox.Enabled = true;
                polyglotwebaccessTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                polyglotwebaccessTempDatebox.Enabled = false;
                polyglotwebaccessTempHelpdeskEndDatebox.Enabled = false;
                polyglotwebaccessTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //57
        private void magservercomponentsConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magservercomponentsConstantAmountTextbox.Text)) && (int.Parse(magservercomponentsConstantAmountTextbox.Text) > 0))
            {
                magservercomponentsConstantHelpdeskEndDatebox.Enabled = true;
                magservercomponentsConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magservercomponentsConstantHelpdeskEndDatebox.Enabled = false;
                magservercomponentsConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void magservercomponentsTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(magservercomponentsTempAmountTextbox.Text)) && (int.Parse(magservercomponentsTempAmountTextbox.Text) > 0))
            {
                magservercomponentsTempDatebox.Enabled = true;
                magservercomponentsTempHelpdeskEndDatebox.Enabled = true;
                magservercomponentsTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                magservercomponentsTempDatebox.Enabled = false;
                magservercomponentsTempHelpdeskEndDatebox.Enabled = false;
                magservercomponentsTempGuaranteeEndDatebox.Enabled = false;
            }
        }
        
        //58
        private void rampaaudioTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(rampaaudioTempAmountTextbox.Text)) && (int.Parse(rampaaudioTempAmountTextbox.Text) > 0))
                rampaaudioTempDatebox.Enabled = true;
            else
                rampaaudioTempDatebox.Enabled = false;
        }

        //59
        private void gatetele2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gatetele2TempAmountTextbox.Text)) && (int.Parse(gatetele2TempAmountTextbox.Text) > 0))
                gatetele2TempDatebox.Enabled = true;
            else
                gatetele2TempDatebox.Enabled = false;
        }

        //60
        private void onvifdeviceTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(onvifdeviceTempAmountTextbox.Text)) && (int.Parse(onvifdeviceTempAmountTextbox.Text) > 0))
                onvifdeviceTempDatebox.Enabled = true;
            else
                onvifdeviceTempDatebox.Enabled = false;
        }

        //61
        private void oracleserverbaseConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(oracleserverbaseConstantAmountTextbox.Text)) && (int.Parse(oracleserverbaseConstantAmountTextbox.Text) > 0))
            {
                oracleserverbaseConstantHelpdeskEndDatebox.Enabled = true;
                oracleserverbaseConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                oracleserverbaseConstantHelpdeskEndDatebox.Enabled = false;
                oracleserverbaseConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void oracleserverbaseTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(oracleserverbaseTempAmountTextbox.Text)) && (int.Parse(oracleserverbaseTempAmountTextbox.Text) > 0))
            {
                oracleserverbaseTempDatebox.Enabled = true;
                oracleserverbaseTempHelpdeskEndDatebox.Enabled = true;
                oracleserverbaseTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                oracleserverbaseTempDatebox.Enabled = false;
                oracleserverbaseTempHelpdeskEndDatebox.Enabled = false;
                oracleserverbaseTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //62
        private void oracleworkstationbaseConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(oracleworkstationbaseConstantAmountTextbox.Text)) && (int.Parse(oracleworkstationbaseConstantAmountTextbox.Text) > 0))
            {
                oracleworkstationbaseConstantHelpdeskEndDatebox.Enabled = true;
                oracleworkstationbaseConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                oracleworkstationbaseConstantHelpdeskEndDatebox.Enabled = false;
                oracleworkstationbaseConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void oracleworkstationbaseTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(oracleworkstationbaseTempAmountTextbox.Text)) && (int.Parse(oracleworkstationbaseTempAmountTextbox.Text) > 0))
            {
                oracleworkstationbaseTempDatebox.Enabled = true;
                oracleworkstationbaseTempHelpdeskEndDatebox.Enabled = true;
                oracleworkstationbaseTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                oracleworkstationbaseTempDatebox.Enabled = false;
                oracleworkstationbaseTempHelpdeskEndDatebox.Enabled = false;
                oracleworkstationbaseTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //63
        private void hatamagTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(hatamagTempAmountTextbox.Text)) && (int.Parse(hatamagTempAmountTextbox.Text) > 0))
                hatamagTempDatebox.Enabled = true;
            else
                hatamagTempDatebox.Enabled = false;
        }

        //64
        private void billclientg2ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(billclientg2ConstantAmountTextbox.Text)) && (int.Parse(billclientg2ConstantAmountTextbox.Text) > 0))
            {
                billclientg2ConstantHelpdeskEndDatebox.Enabled = true;
                billclientg2ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                billclientg2ConstantHelpdeskEndDatebox.Enabled = false;
                billclientg2ConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void billclientg2TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(billclientg2TempAmountTextbox.Text)) && (int.Parse(billclientg2TempAmountTextbox.Text) > 0))
            {
                billclientg2TempDatebox.Enabled = true;
                billclientg2TempHelpdeskEndDatebox.Enabled = true;
                billclientg2TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                billclientg2TempDatebox.Enabled = false;
                billclientg2TempHelpdeskEndDatebox.Enabled = false;
                billclientg2TempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //65
        private void gateConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateConstantAmountTextbox.Text)) && (int.Parse(gateConstantAmountTextbox.Text) > 0))
            {
                gateConstantHelpdeskEndDatebox.Enabled = true;
                gateConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                gateConstantHelpdeskEndDatebox.Enabled = false;
                gateConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void gateTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(gateTempAmountTextbox.Text)) && (int.Parse(gateTempAmountTextbox.Text) > 0))
            {
                gateTempDatebox.Enabled = true;
                gateTempHelpdeskEndDatebox.Enabled = true;
                gateTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                gateTempDatebox.Enabled = false;
                gateTempHelpdeskEndDatebox.Enabled = false;
                gateTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //66
        private void kolpakConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(kolpakConstantAmountTextbox.Text)) && (int.Parse(kolpakConstantAmountTextbox.Text) > 0))
            {
                kolpakConstantHelpdeskEndDatebox.Enabled = true;
                kolpakConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                kolpakConstantHelpdeskEndDatebox.Enabled = false;
                kolpakConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void kolpakTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(kolpakTempAmountTextbox.Text)) && (int.Parse(kolpakTempAmountTextbox.Text) > 0))
            {
                kolpakTempDatebox.Enabled = true;
                kolpakTempHelpdeskEndDatebox.Enabled = true;
                kolpakTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                kolpakTempDatebox.Enabled = false;
                kolpakTempHelpdeskEndDatebox.Enabled = false;
                kolpakTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //67
        private void playbackminingConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(playbackminingConstantAmountTextbox.Text)) && (int.Parse(playbackminingConstantAmountTextbox.Text) > 0))
            {
                playbackminingConstantHelpdeskEndDatebox.Enabled = true;
                playbackminingConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                playbackminingConstantHelpdeskEndDatebox.Enabled = false;
                playbackminingConstantGuaranteeEndDatebox.Enabled = false;
            }
        }

        private void playbackminingTempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(playbackminingTempAmountTextbox.Text)) && (int.Parse(playbackminingTempAmountTextbox.Text) > 0))
            {
                playbackminingTempDatebox.Enabled = true;
                playbackminingTempHelpdeskEndDatebox.Enabled = true;
                playbackminingTempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                playbackminingTempDatebox.Enabled = false;
                playbackminingTempHelpdeskEndDatebox.Enabled = false;
                playbackminingTempGuaranteeEndDatebox.Enabled = false;
            }
        }

        //здесь добавление инфы о новых лицензиях
        /*
        //
        private void TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(TempAmountTextbox.Text)) && (int.Parse(TempAmountTextbox.Text) > 0))
                TempDatebox.Enabled = true;
            else
                TempDatebox.Enabled = false;
        }
         */
        /*
        //
        private void ConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(ConstantAmountTextbox.Text)) && (int.Parse(ConstantAmountTextbox.Text) > 0))
            {
                ConstantHelpdeskEndDatebox.Enabled = true;
                ConstantGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                ConstantHelpdeskEndDatebox.Enabled = false;
                ConstantGuaranteeEndDatebox.Enabled = false;
            }
        }
        
        private void TempAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(TempAmountTextbox.Text)) && (int.Parse(TempAmountTextbox.Text) > 0))
            {
                TempDatebox.Enabled = true;
                TempHelpdeskEndDatebox.Enabled = true;
                TempGuaranteeEndDatebox.Enabled = true;
            }
            else
            {
                TempDatebox.Enabled = false;
                TempHelpdeskEndDatebox.Enabled = false;
                TempGuaranteeEndDatebox.Enabled = false;
            }
        }
        */
        #endregion

        #region ограничение на ввод символов (только цифры)
        private void mainKeyNumberTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void reserveKeyNumberTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //1
        private void magreadConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void magreadTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //2
        private void taskjournalConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void taskjournalTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //3
        private void magfaxConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void magfaxTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //4
        private void trackingConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trackingTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //5
        private void audioplayerConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void audioplayerTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //6
        private void billclientConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void billclientTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //7
        private void mwieserverConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void mwieserverTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //8
        private void mwieclientConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void mwieclientTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //9
        private void mwiestationConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void mwiestationTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //10
        private void integrationserverConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void integrationserverTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //11
        private void integrationadapterConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void integrationadapterTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //12
        private void videojournalConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void videojournalTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //13
        private void imitatorsormConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void imitatorsormTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //14
        private void maggradientConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void maggradientTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //15
        private void tracking3ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void tracking3TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //16
        private void gatemegafonConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatemegafonTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //17
        private void gatemtsConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatemtsTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //18
        private void gatebeelineConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatebeelineTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //19
        private void istokclientConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void istokclientTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //20
        private void higinaconverterConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void higinaconverterTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //21
        private void webmapsConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void webmapsTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //22
        private void gateYanvarConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gateYanvarTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //23
        private void play3gvideoConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void play3gvideoTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //24
        private void integrationnetconfigConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void integrationnetconfigTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //25
        private void trackingg2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trackingg2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //26
        private void webmapsg2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void webmapsg2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //27
        private void magreadg2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void magreadg2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //28
        private void videojournalg2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void videojournalg2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //29
        private void gateutkConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gateutkTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //30
        private void smsserviceConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void smsserviceTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //31
        private void simserviceConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void simserviceTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //32
        private void barcodesadminConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void barcodesadminTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //33
        private void gatesmartsConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatesmartsTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //34
        private void simmagplaceserviceConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void simmagplaceserviceTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //35
        private void armnvdConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void armnvdTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //36
        private void devicecontrolserverConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void devicecontrolserverTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //37
        private void trassatestConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trassatestTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //38
        private void trassavkpruConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trassavkpruTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //39
        private void trassavkputcpipConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trassavkputcpipTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //40
        private void trassavkpputcpipConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void trassavkpputcpipTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //41
        private void gateservicevkConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gateservicevkTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //42
        private void avk4ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void avk4TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //43
        private void magbiyaconnectionConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void magbiyaconnectionTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //44
        private void voiceidentificationConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void voiceidentificationTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //45
        private void mobileobjectstrackingConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void mobileobjectstrackingTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //46
        private void barcodek2pConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void barcodek2pTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //47
        private void barcodek2rConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void barcodek2rTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //48
        private void barcodek2fConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void barcodek2fTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //49
        private void accountingbarcodeprintingConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void accountingbarcodeprintingTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //50
        private void analyzestatisticserviceConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void analyzestatisticserviceTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //51
        private void avk2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void avk2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }


        //52
        private void katunclientConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void katunclientTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //53
        private void gatenssConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatenssTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //54
        private void avk2uConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void avk2uTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //55
        private void gateistokConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gateistokTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //56
        private void polyglotwebaccessConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void polyglotwebaccessTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //57
        private void magservercomponentsConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void magservercomponentsTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //58
        private void rampaaudioConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void rampaaudioTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //59
        private void gatetele2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gatetele2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //60
        private void onvifdeviceConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void onvifdeviceTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //61
        private void oracleserverbaseConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void oracleserverbaseTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //62
        private void oracleworkstationbaseConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void oracleworkstationbaseTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //63
        private void hatamagConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void hatamagTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //64
        private void billclientg2ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void billclientg2TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //65
        private void gateConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void gateTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //66
        private void kolpakConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void kolpakTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //67
        private void playbackminingConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void playbackminingTempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        //здесь добавление инфы о новых лицензиях
        /*
        //
        private void ConstantAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void TempAmountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
         */
        #endregion

        #region формирование единой лицензии МАГ-РСВО
        private void sameMagreadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sameMagreadCheckBox.Checked)
            {
                audioplayerConstantAmountTextbox.Text = magreadConstantAmountTextbox.Text;
                audioplayerTempAmountTextbox.Text = magreadTempAmountTextbox.Text;
                audioplayerTempDatebox.Value = magreadTempDatebox.Value;

                audioplayerConstantAmountTextbox.Enabled = false;
                audioplayerTempAmountTextbox.Enabled = false;
                audioplayerTempDatebox.Enabled = false;
            }
            else
            {
                audioplayerConstantAmountTextbox.Enabled = true;
                audioplayerTempAmountTextbox.Enabled = true;
                if ((!String.IsNullOrEmpty(audioplayerTempAmountTextbox.Text)) && (int.Parse(audioplayerTempAmountTextbox.Text) > 0))
                    audioplayerTempDatebox.Enabled = true;
                else
                    audioplayerTempDatebox.Enabled = false;
            }
        }

        private void magreadConstantAmountTextbox_TextChanged(object sender, EventArgs e)
        {
            if (sameMagreadCheckBox.Checked)
            {
                audioplayerConstantAmountTextbox.Text = magreadConstantAmountTextbox.Text;
            }
        }

        private void magreadTempDatebox_ValueChanged(object sender, EventArgs e)
        {
            if (sameMagreadCheckBox.Checked)
            {
                audioplayerTempDatebox.Value = magreadTempDatebox.Value;
            }
        }
        #endregion

        #region кнопки открытия истории МАГ 7
        
        //25
        private void trackingg2HistoryBtn_Click(object sender, EventArgs e)
        {
            
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[24].EntriesHistory);
            g2LicencesHistory.Text = trackingg2Group.Text;
            g2LicencesHistory.Show();
        }

        //26
        private void webmapsg2HistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[25].EntriesHistory);
            g2LicencesHistory.Text = webmapsg2Group.Text;
            g2LicencesHistory.Show();
        }

        //27
        private void magreadg2HistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[26].EntriesHistory);
            g2LicencesHistory.Text = magreadg2Group.Text;
            g2LicencesHistory.Show();
        }

        //28
        private void videojournalg2HistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[27].EntriesHistory);
            g2LicencesHistory.Text = videojournalg2Group.Text;
            g2LicencesHistory.Show();
        }

        //30
        private void smsserviceHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[29].EntriesHistory);
            g2LicencesHistory.Text = smsserviceGroup.Text;
            g2LicencesHistory.Show();
        }

        //31
        private void simserviceHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[30].EntriesHistory);
            g2LicencesHistory.Text = simserviceGroup.Text;
            g2LicencesHistory.Show();
        }

        //32
        private void barcodesadminHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[31].EntriesHistory);
            g2LicencesHistory.Text = barcodesadminGroup.Text;
            g2LicencesHistory.Show();
        }

        //34
        private void simmagplaceserviceHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[33].EntriesHistory);
            g2LicencesHistory.Text = simmagplaceserviceGroup.Text;
            g2LicencesHistory.Show();
        }

        //35
        private void armnvdHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[34].EntriesHistory);
            g2LicencesHistory.Text = armnvdGroup.Text;
            g2LicencesHistory.Show();
        }

        //36
        private void devicecontrolserverHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[35].EntriesHistory);
            g2LicencesHistory.Text = devicecontrolserverGroup.Text;
            g2LicencesHistory.Show();
        }

        //43
        private void magbiyaconnectionHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[42].EntriesHistory);
            g2LicencesHistory.Text = magbiyaconnectionGroup.Text;
            g2LicencesHistory.Show();
        }

        //44
        private void voiceidentificationHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[43].EntriesHistory);
            g2LicencesHistory.Text = voiceidentificationGroup.Text;
            g2LicencesHistory.Show();
        }

        //45
        private void mobileobjectstrackingHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[44].EntriesHistory);
            g2LicencesHistory.Text = mobileobjectstrackingGroup.Text;
            g2LicencesHistory.Show();
        }

        //46
        private void barcodek2pHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[45].EntriesHistory);
            g2LicencesHistory.Text = barcodek2pGroup.Text;
            g2LicencesHistory.Show();
        }

        //47
        private void barcodek2rHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[46].EntriesHistory);
            g2LicencesHistory.Text = barcodek2rGroup.Text;
            g2LicencesHistory.Show();
        }

        //48
        private void barcodek2fHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[47].EntriesHistory);
            g2LicencesHistory.Text = barcodek2fGroup.Text;
            g2LicencesHistory.Show();
        }

        //49
        private void accountingbarcodeprintingHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[48].EntriesHistory);
            g2LicencesHistory.Text = accountingbarcodeprintingGroup.Text;
            g2LicencesHistory.Show();
        }
        
        //50
        private void analyzestatisticserviceHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[49].EntriesHistory);
            g2LicencesHistory.Text = analyzestatisticserviceGroup.Text;
            g2LicencesHistory.Show();
        }

        //56
        private void polyglotwebaccessHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[55].EntriesHistory);
            g2LicencesHistory.Text = polyglotwebaccessGroup.Text;
            g2LicencesHistory.Show();
        }

        //57
        private void magservercomponentsHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[56].EntriesHistory);
            g2LicencesHistory.Text = magbiyaconnectionGroup.Text;
            g2LicencesHistory.Show();
        }

        //61
        private void oracleserverbaseHistoryBtn_Click(object sender, EventArgs e)
        {
             G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[60].EntriesHistory);
             g2LicencesHistory.Text = oracleserverbaseGroup.Text;
             g2LicencesHistory.Show();
        }

        //62
        private void oracleworkstationbaseHistoryBtn_Click(object sender, EventArgs e)
         {
             G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[61].EntriesHistory);
             g2LicencesHistory.Text = oracleworkstationbaseGroup.Text;
             g2LicencesHistory.Show();
         }

        //64
        private void billclientg2HistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[63].EntriesHistory);
            g2LicencesHistory.Text = billclientg2Group.Text;
            g2LicencesHistory.Show();
        }

        //65
        private void gateHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[64].EntriesHistory);
            g2LicencesHistory.Text = gateGroup.Text;
            g2LicencesHistory.Show();
        }

        //66
        private void kolpakHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[65].EntriesHistory);
            g2LicencesHistory.Text = kolpakGroup.Text;
            g2LicencesHistory.Show();
        }

        //67
        private void playbackminingHistoryBtn_Click(object sender, EventArgs e)
        {
            G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[66].EntriesHistory);
            g2LicencesHistory.Text = playbackminingGroup.Text;
            g2LicencesHistory.Show();
        }

        //здесь добавление инфы о новых лицензиях
        /*
         //
         private void HistoryBtn_Click(object sender, EventArgs e)
         {
             G2LicencesHistory g2LicencesHistory = new G2LicencesHistory(licenceInfo.licences[].EntriesHistory);
             g2LicencesHistory.Text = Group.Text;
             g2LicencesHistory.Show();
         }
         */
        #endregion

        //для активации выбранной вкладки
        private void dataTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveControl = dataTab.SelectedTab;
        }

    }
        #endregion
}