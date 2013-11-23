namespace Repository.Business.Protocols{
    public class ProtocolStack {
        public string key { get; set; }
        public Protocol Create { get; private set; }
        public Protocol Update { get; private set; }
        public Protocol Delete { get; private set; }

        private ProtocolStack() {}

        public static ProtocolStack WithPremise(Premise premise, string key = "") {
            ProtocolStack constructing = new ProtocolStack();
            Premise createPremise, updatePremise, deletePremise;

            createPremise = premise.Clone();

            updatePremise = premise.Clone();
            updatePremise.nullable = true;

            deletePremise = new Premise();

            constructing.Create = Protocol.WithPremise(createPremise, key);
            constructing.Update = Protocol.WithPremise(updatePremise, key);
            constructing.Delete = Protocol.WithPremise(deletePremise, key);

            return constructing;
        }

        public static ProtocolStack ForUsername() {
            return ProtocolStack.WithPremise(
                new Premise { 
                    hidden = true,
                    maxLength = 32,
                    nullable = false
                },
                "username"
            );
        }

        public static ProtocolStack ForKey(string key = "") {
            return ProtocolStack.WithPremise(
                new Premise { 
                    hidden = true
                },
                key
            );
        }
    }
}
