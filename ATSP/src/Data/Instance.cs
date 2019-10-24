namespace ATSP.Data
{
    public class Instance
    {
        public Instance()
        {
            Data = new Graph();
        }

        public int N { get => Data.vertices?.Length ?? 0; }
        public Graph Data { get; set; }
    }
}