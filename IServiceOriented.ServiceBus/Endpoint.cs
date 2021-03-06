﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.Serialization;

namespace IServiceOriented.ServiceBus
{
    /// <summary>
    /// Base class for service bus endpoints.
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Endpoint
    {
        protected Endpoint(Guid id, string name, string configurationName, string address, Type contractType, bool transient, DateTime? expiration) : this(id, name, configurationName, address, contractType)            
        {
            Transient = transient;
            Expiration = expiration;
        }

        protected Endpoint(Guid id, string name, string configurationName, string address, string contractTypeName, bool transient, DateTime? expiration)
            : this(id, name, configurationName, address, contractTypeName)
        {
            Transient = transient;
            Expiration = expiration;
        }

       
        protected Endpoint(Guid id, string name, string configurationName, string address, Type contractType)
        {
            Id = id;
            Name = name;
            ConfigurationName = configurationName;
            Address = address;
            ContractType = contractType;
        }

        protected Endpoint(Guid id, string name, string configurationName, string address, string contractTypeName)
        {
            Id = id;
            Name = name;
            ConfigurationName = configurationName;
            Address = address;
            ContractTypeName = contractTypeName;
        }

        Guid _endpointId = Guid.NewGuid();
        
        /// <summary>
        /// Gets the unique identifier of the endpoint.
        /// </summary>
        [DataMember]
        public Guid Id
        {
            get
            {
                return _endpointId;
            }
            private set
            {
                _endpointId = value;
            }
        }

        Type _contractType;                
        /// <summary>
        /// Gets the Type of the contract supported by the endpoint.
        /// </summary>
        public Type ContractType
        {
            get
            {
                return _contractType;
            }
            private set
            {
                _contractType = value;
            }
        }

        /// <summary>
        /// Gets the name of the contract type supported by the endpoint.
        /// </summary>
        [DataMember]
        public string ContractTypeName
        {
            get
            {
                if (_contractType == null)
                {
                    return null;    
                }
                else
                {
                    return _contractType.AssemblyQualifiedName;
                }
            }
            private set
            {
                _contractType = Type.GetType(value);
                if (_contractType == null)
                {
                    throw new InvalidOperationException(value + " is an invalid type");
                }
            }
        }

        string _address;
        /// <summary>
        /// Gets the address of the endpoint.
        /// </summary>
        [DataMember]
        public string Address
        {
            get
            {
                return _address;
            }
            private set
            {
                _address = value;
            }
        }

        string _configurationName;
        /// <summary>
        /// Gets the name of the configuration used by the endpoint.
        /// </summary>
        [DataMember]
        public string ConfigurationName
        {
            get
            {
                return _configurationName;
            }
            private set
            {
                _configurationName = value;
            }
        }

        string _name;
        /// <summary>
        /// Gets the name of this endpoint.
        /// </summary>
        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        bool _transient;
        /// <summary>
        /// Gets a boolean value indicating whether this endpoint is transient (true) or should be persisted (false).
        /// </summary>
        [DataMember]
        public bool Transient
        {
            get
            {
                return _transient;
            }
            private set
            {
                _transient = value;
            }
        }

        DateTime? _expiration;

        [DataMember]
        public DateTime? Expiration
        {
            get
            {
                return _expiration;
            }
            private set
            {
                _expiration = value;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (_expiration.HasValue)
                {
                    return _expiration.Value < DateTime.Now;
                }
                else
                {
                    return false;
                }
            }
        }
    }    
}
