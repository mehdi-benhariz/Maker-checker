namespace maker_checker_v1.models.entities
{
    public class Request
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; } = 0;
        public string Status { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public ValidationProgress ValidationProgress { get; set; }
        public Request(string name, int serviceTypeId, float amount = 0, string status = "Pending")
        {
            this.Id = nbr++;
            this.Name = name;
            this.Amount = amount;
            this.Status = status;
            this.ServiceTypeId = serviceTypeId;
            ValidationProgress = new ValidationProgress(this.Id);
            initValidationProgress();

        }

        private void initValidationProgress()
        {
            //init validation progress from servicesType validation rules
            //get the service type by id
            var _requestDataStore = new RequestDataStore();
            var serviceType = _requestDataStore.ServiceTypes.FirstOrDefault(s => s.Id == this.ServiceTypeId);
            if (serviceType == null)
                throw new System.Exception("Service type not found");

            foreach (var rule in serviceType.validation.rules)
                ValidationProgress.rules.Add(rule);

        }
    }
}