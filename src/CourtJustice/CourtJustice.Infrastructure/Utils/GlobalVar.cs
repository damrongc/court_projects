namespace CourtJustice.Infrastructure.Utils
{
    public static class GlobalVar
    {
        static string? _globalUserId;
        public static string GlobalUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_globalUserId))
                {

                    switch (GlobalProcessCode)
                    {


                        case 1:
                            return "prd.user";
                        case 2:
                            return "ing.user";
                        case 3:
                            return "pac.user";
                        default: return "handheld";

                    }

                }
                else
                {
                    return _globalUserId;
                }
            }
            set
            {
                _globalUserId = value;
            }
        }

        static short _globalPlantId;
        public static short GlobalPlantId
        {
            get
            {
                if (_globalPlantId == 0)
                {
                    return 1;
                }
                else
                {
                    return _globalPlantId;
                }

            }
            set
            {
                _globalPlantId = value;
            }
        }

        static short _globalProcessCode;
        public static short GlobalProcessCode
        {
            get
            {
                if (_globalProcessCode == 0)
                {
                    return 1;
                }
                else
                {
                    return _globalProcessCode;
                }

            }
            set
            {
                _globalProcessCode = value;
            }
        }

        static short _globalGroupId;
        public static short GlobalGroupId
        {
            get
            {
                if (_globalGroupId == 0)
                {
                    return 1;
                }
                else
                {
                    return _globalGroupId;
                }

            }
            set
            {
                _globalGroupId = value;
            }
        }



    }

    public enum RunningCode { Packing, ExtraCode, BarCodeId, PalletNo, PutAway, Transfer, ConfirmIssue, Receive, Picking, QC, InTrans, Return, Purchase, Adjust };
    public enum TransState { New, Edit };
    public enum IssueDocType { None, Sale, Repack, Reprocess, QC, Damage, QC1, Produce, Others }
    public enum DocType { Production, Purchase, Issue, Return, Receive, Picking, Stock }

    public sealed class ControlChars
    {
        public const char Back = '\b';
        public const char Cr = '\r';
        public const string CrLf = "\r\n";
        public const char FormFeed = '\f';
        public const char Lf = '\n';
        public const string NewLine = "\r\n";
        public const char NullChar = '\0';
        public const char Quote = '"';
        public const char Tab = '\t';
        public const char VerticalTab = '\v';
        public const char Comma = ',';
    }

}
