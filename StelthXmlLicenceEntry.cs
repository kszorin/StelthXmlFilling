using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
    класс, описывающий дополнительные параметры, относящиеся к лицензиям типа G2
    при добавлении новых лицензий, изменять не требуется
*/

namespace StelthXmlFilling
{
    public partial class StelthXmlLicenceEntry
    {
        private int licenсes;                       //количество лицензий
        private DateTime purchaseDate;              //дата покупки лицензии
        private DateTime expirationDate;            //дата окончания лицензии
        private DateTime supportExpirationDate;     //дата окончания тех.поддержки
        private DateTime guaranteeExpirationDate;   //дата окончания гарантийной поддержки
        private bool isTemp;                        //временная ли лицензия

        public StelthXmlLicenceEntry()
        {
            licenсes = -1;
            isTemp = false;
            purchaseDate = DateTime.Now;
            expirationDate = DateTime.Now;
            supportExpirationDate = DateTime.Now;
            guaranteeExpirationDate = DateTime.Now;
        }

        public StelthXmlLicenceEntry(int setAmountLicences, DateTime setPurchaseDate,
            DateTime setExpirationDate, DateTime setSupportExpirationDate, DateTime setGuaranteeExpirationDate)
        {
            licenсes = setAmountLicences;
            isTemp = true;
            purchaseDate = setPurchaseDate;
            expirationDate = setExpirationDate;
            supportExpirationDate = setSupportExpirationDate;
            guaranteeExpirationDate = setGuaranteeExpirationDate;
        }

        public StelthXmlLicenceEntry(int setAmountLicences, DateTime setPurchaseDate,
            DateTime setSupportExpirationDate, DateTime setGuaranteeExpirationDate)
        {
            licenсes = setAmountLicences;
            isTemp = false;
            purchaseDate = setPurchaseDate;
            supportExpirationDate = setSupportExpirationDate;
            guaranteeExpirationDate = setGuaranteeExpirationDate;
        }

        public int Licenсes
        {
            get
            {
                return licenсes;
            }
            set
            {
                licenсes = value;
            }
        }

        public bool IsTemp
        {
            get
            {
                return isTemp;
            }
            set
            {
                isTemp = value;
            }
        }

        public DateTime PurchaseDate
        {
            get
            {
                return purchaseDate;
            }
            set
            {
                purchaseDate = value;
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

        public DateTime SupportExpirationDate
        {
            get
            {
                return supportExpirationDate;
            }
            set
            {
                supportExpirationDate = value;
            }
        }

        public DateTime GuaranteeExpirationDate
        {
            get
            {
                return guaranteeExpirationDate;
            }
            set
            {
                guaranteeExpirationDate = value;
            }
        }    
    }
}
