using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateEntityServices.Modules.CorporateEntityModule.Entities;

namespace CorporateEntityServices.Modules.CorporateEntityModule.Store
{
    public static class CorporateEntityDataStore
    {
        public static BlockingCollection<CorporateEntity> DataStore = new BlockingCollection<CorporateEntity>()
        {
            new CorporateEntity() {Id = 1,InternalReference = Guid.NewGuid(),Name = "Zags!"},
            new CorporateEntity() {Id = 2,InternalReference = Guid.NewGuid(),Name = "Protegys"}
        };

        public static void Add(CorporateEntity entity)
        {
            DataStore.Add(entity);
        }

        public static void Modify(CorporateEntity entity)
        {
            var item = DataStore.FirstOrDefault(e => e.Id == entity.Id);
            if (item != null)
            {
                if (!entity.InternalReference.Equals(Guid.Empty))
                {
                    item.InternalReference = entity.InternalReference;
                }
                item.Name = entity.Name;
            }
        }

        public static IEnumerable<CorporateEntity> get(Func<CorporateEntity, bool> predicate)
        {
            return DataStore.Where(predicate).ToList();
        }
        public static IEnumerable<CorporateEntity> get(int startIndex,int length)
        {
            return DataStore.Skip(startIndex).Take(length).ToList();
        }
    }
}