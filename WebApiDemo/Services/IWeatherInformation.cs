using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public interface IWeatherInformation
    {
        public Task<Rootobject> GetWheatherInformation();    
    }
}
