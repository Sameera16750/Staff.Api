﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Staff.Application.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MessageResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Staff.Application.Resources.MessageResources", typeof(MessageResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string SAVE_SUCCESS {
            get {
                return ResourceManager.GetString("SAVE_SUCCESS", resourceCulture);
            }
        }
        
        internal static string SAVE_FAILD {
            get {
                return ResourceManager.GetString("SAVE_FAILD", resourceCulture);
            }
        }
        
        internal static string DATA_NOT_FOUND {
            get {
                return ResourceManager.GetString("DATA_NOT_FOUND", resourceCulture);
            }
        }
        
        internal static string INTERNAL_ERROR {
            get {
                return ResourceManager.GetString("INTERNAL_ERROR", resourceCulture);
            }
        }
        
        internal static string UPDATE_SUCCESS {
            get {
                return ResourceManager.GetString("UPDATE_SUCCESS", resourceCulture);
            }
        }
        
        internal static string UPDATE_FAILED {
            get {
                return ResourceManager.GetString("UPDATE_FAILED", resourceCulture);
            }
        }
        
        internal static string DELETE_FAILED {
            get {
                return ResourceManager.GetString("DELETE_FAILED", resourceCulture);
            }
        }
        
        internal static string DELETE_SUCCESS {
            get {
                return ResourceManager.GetString("DELETE_SUCCESS", resourceCulture);
            }
        }
        
        internal static string INVALID_ORGANIZATION {
            get {
                return ResourceManager.GetString("INVALID_ORGANIZATION", resourceCulture);
            }
        }
        
        internal static string INVALID_DEPARTMENT {
            get {
                return ResourceManager.GetString("INVALID_DEPARTMENT", resourceCulture);
            }
        }
    }
}
