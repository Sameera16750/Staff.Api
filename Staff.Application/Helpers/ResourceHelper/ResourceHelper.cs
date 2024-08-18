using System.Reflection;
using System.Resources;
using Staff.Core.Constants;

namespace Staff.Application.Helpers.ResourceHelper;

public abstract class ResourceHelper
{
    #region Message Resources
    
    public class MessageResourceHelper:IMessageResourceHelper
    {
        private readonly ResourceManager _resourceManager=new ResourceManager(Constants.Files.Messages,Assembly.GetExecutingAssembly());
        public string GetResource(string key)
        {
            return _resourceManager.GetString(key)??string.Empty;
        }
    }

    #endregion
}