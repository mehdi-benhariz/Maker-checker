

namespace maker_checker_v1.models.entities
{
    public class ValidationProgress
    {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public ICollection<Operation> Operations = new List<Operation>();
        //todo optimize it
        public byte Progress()
        {
            int count = Operations.Count();
            int nbrOfStaff = Request.ServiceType.RquiredNbrOfStaff();

            return Convert.ToByte(count * 100 / nbrOfStaff);
        }
        public bool IsValidated
        {
            get
            {//todo:change it later
                return Progress() == 100;
            }
        }

        public ValidationProgress(int requestId)
        {
            this.RequestId = requestId;
        }
        // initRules();

    }
}