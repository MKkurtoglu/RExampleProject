using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class CarImageDto
    {
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public int ModelYear { get; set; }
        public string ColorName { get; set; }
        public string Description { get; set; }
        public int DailyPrice { get; set; }
        public List<string> ImagePaths { get; set; }
        public bool IsRented { get; set; } = false;
    }
}
