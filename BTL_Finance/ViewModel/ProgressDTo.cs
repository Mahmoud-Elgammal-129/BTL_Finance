namespace BTL_Finance.ViewModel
{
        public class ProgressDTo
        {
        
            public string Name_progress { get; set; }
            public string RequestName { get; set; }
            public string CompanyName { get; set; }
            public bool Reqest_Status { get; set; } = true;
            public bool Order_Sheet_Status { get; set; }
            public bool Delivery_Note_Status { get; set; }
            public bool Quote_Status { get; set; }
            public bool PO_Status { get; set; }
            public bool Invoice_Status { get; set; }

        }
}
