namespace Rookian.TNL.Features.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            IndexSubViewModel = new IndexSubViewModel(){ Description = "Test134"};
        }
        public string Name { get; set; }
        public IndexSubViewModel IndexSubViewModel { get; set; }
    }

    public class IndexSubViewModel
    {
        public string Description { get; set; } 
    }
}