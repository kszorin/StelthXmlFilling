using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
    класс, описывающий лицензию
    при добавлении новых лицензий, изменять не требуется
*/

namespace StelthXmlFilling
{
    class StelthXmlLicence
    {
        private int number;                                 //номер лицензии
        private String name;                                //имя лицензии
        private String rusName;                             //имя лицензии по-русски
        private int version;                                //версия лицензии
        private int constantAmount;                         //количество постоянных лицензий
        private int tempAmount;                             //количество временных лицензий
        private DateTime expirationDate;                    //срок временной лицензии
        private List<StelthXmlLicenceEntry> entriesHistory; //история прошивок лицензии
        private StelthXmlLicenceEntry entryConstant;        //текущая постоянная лицензия 2-го типа
        private StelthXmlLicenceEntry entryTemp;            //текущая временная лицензия 2-го типа

        //общие поля формы
        private System.Windows.Forms.TextBox constantAmountTextbox;
        private System.Windows.Forms.TextBox tempAmountTextbox;
        private System.Windows.Forms.DateTimePicker tempDatebox;
        
        //поля формы для лицензий 2го типа
        private System.Windows.Forms.DateTimePicker constantHelpdeskEndDatebox;
        private System.Windows.Forms.DateTimePicker constantGuaranteeEndDatebox;
        private System.Windows.Forms.DateTimePicker tempHelpdeskEndDatebox;
        private System.Windows.Forms.DateTimePicker tempGuaranteeEndDatebox;
        private System.Windows.Forms.Label constantAmountTotal;
        private System.Windows.Forms.Label tempAmountTotal;
        private System.Windows.Forms.Button historyBtn;

          //public System.Windows.Forms.GroupBox group;

          //подумать над тем, как передавать целиком контейней-группу для лицензии, а не все поля. Тогда доступаться будет проще.



          public StelthXmlLicence()
        {
            number = -1;
            name = "";
            version = -1;
            constantAmount = 0;
            tempAmount = 0;
            expirationDate = DateTime.Now;
            entriesHistory = new List<StelthXmlLicenceEntry>();
        }

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public String RusName
        {
            get
            {
                return rusName;
            }
            set
            {
                rusName = value;
            }
        }

        public int Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public int ConstantAmount
        {
            get
            {
                return constantAmount;
            }
            set
            {
                constantAmount = value;
            }
        }

        public int TempAmount
        {
            get
            {
                return tempAmount;
            }
            set
            {
                tempAmount = value;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return expirationDate;
            }
            set
            {
                expirationDate = value;
            }
        }

        public List<StelthXmlLicenceEntry> EntriesHistory
        {
            get
            {
                return entriesHistory;
            }
            set
            {
                entriesHistory = value;
            }
        }

        public StelthXmlLicenceEntry EntryConstant
        {
            get
            {
                return entryConstant;
            }
            set
            {
                entryConstant = value;
            }
        }

        public StelthXmlLicenceEntry EntryTemp
        {
            get
            {
                return entryTemp;
            }
            set
            {
                entryTemp = value;
            }
        }

        public TextBox ConstantAmountTextbox
        {
            get
            {
                return constantAmountTextbox;
            }

            set
            {
                constantAmountTextbox = value;
            }
        }

        public TextBox TempAmountTextbox
        {
            get
            {
                return tempAmountTextbox;
            }

            set
            {
                tempAmountTextbox = value;
            }
        }

        public DateTimePicker TempDatebox
        {
            get
            {
                return tempDatebox;
            }

            set
            {
                tempDatebox = value;
            }
        }

        public DateTimePicker ConstantHelpdeskEndDatebox
        {
            get
            {
                return constantHelpdeskEndDatebox;
            }

            set
            {
                constantHelpdeskEndDatebox = value;
            }
        }

        public DateTimePicker ConstantGuaranteeEndDatebox
        {
            get
            {
                return constantGuaranteeEndDatebox;
            }

            set
            {
                constantGuaranteeEndDatebox = value;
            }
        }

        public DateTimePicker TempHelpdeskEndDatebox
        {
            get
            {
                return tempHelpdeskEndDatebox;
            }

            set
            {
                tempHelpdeskEndDatebox = value;
            }
        }

        public DateTimePicker TempGuaranteeEndDatebox
        {
            get
            {
                return tempGuaranteeEndDatebox;
            }

            set
            {
                tempGuaranteeEndDatebox = value;
            }
        }

        public Label ConstantAmountTotal
        {
            get
            {
                return constantAmountTotal;
            }

            set
            {
                constantAmountTotal = value;
            }
        }

        public Label TempAmountTotal
        {
            get
            {
                return tempAmountTotal;
            }

            set
            {
                tempAmountTotal = value;
            }
        }

        public Button HistoryBtn
        {
            get
            {
                return historyBtn;
            }

            set
            {
                historyBtn = value;
            }
        }
    }
}
