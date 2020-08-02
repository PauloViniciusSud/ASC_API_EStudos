using System;
using System.Collections.Generic;
using System.Text;

namespace API.Dominio.Dominio
{
   public abstract  class Entity
   {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
