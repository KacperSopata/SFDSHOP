using SFDShop.Application.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.Interfaces
{
    public interface IPdfGeneratorService
    {
        public void CreatingPDF(OrderForListVm order);
    }
}
