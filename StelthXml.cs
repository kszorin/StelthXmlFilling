using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
    Класс, описывающий существующие лицензии
    При добавлении новых лицензий, класс требуется редактировать.
*/

namespace StelthXmlFilling
{
    class StelthXml
    {
        public StelthXmlLicence[] licences;
        public static int LICENCES_AMOUNT = 68;              //общее количество лицензий на текущий момент
        public static int TEMP_LICENCES_TIME = 110;          //стандартный срок временных лицензий
        public static int HELPDESK_TIME = 365 * 2;           //стандартный срок техподдержки
        public static int GUARANTEE_TIME = 365 * 5;          //стандартный срок гарантийной поддержки

        public StelthXml()
        {
            licences = new StelthXmlLicence[LICENCES_AMOUNT];

            for (int i = 0; i < LICENCES_AMOUNT; i++)
            {
                licences[i] = new StelthXmlLicence();
                licences[i].Number = i+1;
            }
            licences[0].Name = "MagRead";
            licences[0].RusName = "Воспроизведение";
            licences[0].Version = 1; 


            licences[1].Name = "TaskJournal";
            licences[1].RusName = "Учёт";
            licences[1].Version = 1;
            
            licences[2].Name = "MagFax";
            licences[2].RusName = "Факс";
            licences[2].Version = 1;
            
            licences[3].Name = "Tracking";
            licences[3].RusName = "Местоположение";
            licences[3].Version = 1;
            
            licences[4].Name = "MagAudioPlayer";
            licences[4].RusName = "Проигрыватель";
            licences[4].Version = 1;
            
            licences[5].Name = "BillClient";
            licences[5].RusName = "РМБия";
            licences[5].Version = 1;
            
            licences[6].Name = "MwieServer";
            licences[6].RusName = "СУ";
            licences[6].Version = 1;
            
            licences[7].Name = "MwieClient";
            licences[7].RusName = "РМИ";
            licences[7].Version = 1;
            
            licences[8].Name = "MwieStation";
            licences[8].RusName = "УМПУ";
            licences[8].Version = 1;
            
            licences[9].Name = "IntegrationServer";
            licences[9].RusName = "Сервис интеграции";
            licences[9].Version = 1;
            
            licences[10].Name = "IntegrationMagAdapter";
            licences[10].RusName = "Адаптер МАГ";
            licences[10].Version = 1;
            
            licences[11].Name = "VideoJournal";
            licences[11].RusName = "Просмотр видео";
            licences[11].Version = 1;
            
            licences[12].Name = "ImitatorSorm";
            licences[12].RusName = "Импульс-2";
            licences[12].Version = 1;
            
            licences[13].Name = "MagGradient";
            licences[13].RusName = "Маг-Градиент";
            licences[13].Version = 1;
            
            licences[14].Name = "Tracking3";
            licences[14].RusName = "Местоположение в.3";
            licences[14].Version = 1;
            
            licences[15].Name = "GateMegafon";
            licences[15].RusName = "Шлюз Мегафон";
            licences[15].Version = 1;
            
            licences[16].Name = "GateMts";
            licences[16].RusName = "Шлюз МТС";
            licences[16].Version = 1;
            
            licences[17].Name = "GateBeeline";
            licences[17].RusName = "Шлюз Билайн";
            licences[17].Version = 1;
            
            licences[18].Name = "IstokClient";
            licences[18].RusName = "Исток Рабочее место";
            licences[18].Version = 1;
            
            licences[19].Name = "HiginaConverter";
            licences[19].RusName = "Конвертер Хижина-МАГ";
            licences[19].Version = 1;
            
            licences[20].Name = "WebMaps";
            licences[20].RusName = "Веб-карты";
            licences[20].Version = 1;
            
            licences[21].Name = "GateYanvar";
            licences[21].RusName = "Шлюз Январь";
            licences[21].Version = 1;
            
            licences[22].Name = "play3gvideo";
            licences[22].RusName = "Видео 3G";
            licences[22].Version = 1;
            
            licences[23].Name = "Integration.SINetCustomization";
            licences[23].RusName = "Конф. сети интеграции";
            licences[23].Version = 1;
            
            licences[24].Name = "CellPhoneTracking";
            licences[24].RusName = "Местоположение G2";
            licences[24].Version = 2;
            
            licences[25].Name = "WebMaps";
            licences[25].RusName = "Веб-карты G2";
            licences[25].Version = 2;
            
            licences[26].Name = "MagRead";
            licences[26].RusName = "Воспроизведение G2";
            licences[26].Version = 2;
            
            licences[27].Name = "VideoJournal";
            licences[27].RusName = "Просмотр видео G2";
            licences[27].Version = 2;
            
            licences[28].Name = "gateUTK";
            licences[28].RusName = "Шлюз ЮТК";
            licences[28].Version = 1;
            
            licences[29].Name = "SmsService";
            licences[29].RusName = "Сим.Сервис приема и отправки сообщений";
            licences[29].Version = 2;
            
            licences[30].Name = "SimService";
            licences[30].RusName = "Сим.Сервис обработки запросов";
            licences[30].Version = 2;
            
            licences[31].Name = "BarCodesAdministrator";
            licences[31].RusName = "Штрих-К.Программа администратора";
            licences[31].Version = 2;
            
            licences[32].Name = "BiyaSmartsGateway";
            licences[32].RusName = "Шлюз Смартс";
            licences[32].Version = 1;
            
            licences[33].Name = "SimMagPlaceService";
            licences[33].RusName = "СИМ-МАГ сервис";
            licences[33].Version = 2;

            licences[34].Name = "ArmNvd";
            licences[34].RusName = "АрмНВД";
            licences[34].Version = 2;

            licences[35].Name = "DeviceControlServer";
            licences[35].RusName = "Сервис управления устройствами";
            licences[35].Version = 2;

            licences[36].Name = "TrassaTest";
            licences[36].RusName = "Трасса Тест";
            licences[36].Version = 1;

            licences[37].Name = "TrassaVkPru";
            licences[37].RusName = "Трасса ВК ПРУ";
            licences[37].Version = 1;

            licences[38].Name = "TrassaVkPuTcpIp";
            licences[38].RusName = "Трасса ВК ПУ TCP IP";
            licences[38].Version = 1;

            licences[39].Name = "TrassaVkPpuTcpIp";
            licences[39].RusName = "Трасса ВК ППУ TCP IP";
            licences[39].Version = 1;

            licences[40].Name = "gateServiceVK";
            licences[40].RusName = "Шлюз Сервис-ВК";
            licences[40].Version = 1;

            licences[41].Name = "AVK4";
            licences[41].RusName = "Рампа-403";
            licences[41].Version = 1;

            licences[42].Name = "MagBiyaConnection";
            licences[42].RusName = "Запросы к Бие";
            licences[42].Version = 2;

            licences[43].Name = "voiceidentification";
            licences[43].RusName = "Интеграция с системой анализа речи";
            licences[43].Version = 2;

            licences[44].Name = "MobileObjectsTracking";
            licences[44].RusName = "Мобильный контроль";
            licences[44].Version = 2;

            licences[45].Name = "BarcodeK2P";
            licences[45].RusName = "Штрих-К.Подготовка документов";
            licences[45].Version = 2;

            licences[46].Name = "BarcodeK2R";
            licences[46].RusName = "Штрих-К.Регистрация документов";
            licences[46].Version = 2;

            licences[47].Name = "BarcodeK2F";
            licences[47].RusName = "Штрих-К-ФСБ";
            licences[47].Version = 2;

            licences[48].Name = "AccountingBarcodePrinting";
            licences[48].RusName = "Учет заданий.Печать штрихкодов";
            licences[48].Version = 2;

            licences[49].Name = "AnalyzeStatisticService";
            licences[49].RusName = "Сервис анализа информации";
            licences[49].Version = 2;

            licences[50].Name = "AVK2";
            licences[50].RusName = "АВК-201";
            licences[50].Version = 1;

            licences[51].Name = "KatunClient";
            licences[51].RusName = "Рабочее место комлекса Катунь";
            licences[51].Version = 1;

            licences[52].Name = "gateNSS";
            licences[52].RusName = "Шлюз НСС";
            licences[52].Version = 1;

            licences[53].Name = "AVK2U";
            licences[53].RusName = "АВК-201У";
            licences[53].Version = 1;

            licences[54].Name = "gateistok";
            licences[54].RusName = "Исток Шлюз";
            licences[54].Version = 1;

            licences[55].Name = "PolyglotWebAccess";
            licences[55].RusName = "Полиглот. Сервис доступа к Веб";
            licences[55].Version = 2;

            licences[56].Name = "MagServerComponents";
            licences[56].RusName = "Компоненты серверного ядра";
            licences[56].Version = 2;

            licences[57].Name = "RampaAudio";
            licences[57].RusName = "Рампа-403(Аудио)";
            licences[57].Version = 1;

            licences[58].Name = "gateTele2";
            licences[58].RusName = "Шлюз Теле2";
            licences[58].Version = 1;

            licences[59].Name = "ONVIFDevice";
            licences[59].RusName = "Устройство ONVIF";
            licences[59].Version = 1;

            licences[60].Name = "OracleServerBase";
            licences[60].RusName = "Оракул. Серверное ядро. Базис";
            licences[60].Version = 2;

            licences[61].Name = "OracleWorkstationBase";
            licences[61].RusName = "Оракул. Рабочее место. Базис";
            licences[61].Version = 2;

            licences[62].Name = "HataMag";
            licences[62].RusName = "Импорт потоков из Хижины";
            licences[62].Version = 1;

            licences[63].Name = "BillClient";
            licences[63].RusName = "Рабочее место комплекса Бия";
            licences[63].Version = 2;

            licences[64].Name = "Gate";
            licences[64].RusName = "Шлюз комплекса Бия";
            licences[64].Version = 2;

            licences[65].Name = "Kolpak";
            licences[65].RusName = "Колпак";
            licences[65].Version = 2;

            licences[66].Name = "PlaybackMining";
            licences[66].RusName = "Обнаружение дополнительной информации";
            licences[66].Version = 2;

            licences[67].Name = "RegDocJournal";
            licences[67].RusName = "Журналы регистрации документов";
            licences[67].Version = 2;

            //здесь добавление инфы о новых лицензиях
        }
    }
}
