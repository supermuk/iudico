using System.Collections.Generic;

namespace IUDICO.Security.ViewModels.Ban
{
    public class BanComputerViewModel
    {
        
        public IList<ComputerWithAttachmentViewModel> Computers { get; set; }

        public BanComputerViewModel()
        {
            this.Computers = new List<ComputerWithAttachmentViewModel>();
        }
    }
}