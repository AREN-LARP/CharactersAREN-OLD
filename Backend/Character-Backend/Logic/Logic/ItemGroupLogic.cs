using DAL.DALInterfaces;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class ItemGroupLogic : IItemGroupLogic
    {
        private readonly IItemGroupRepository _repo;

        public ItemGroupLogic(IItemGroupRepository repo)
        {
            _repo = repo;
        }

        public async Task<ItemGroup> DeleteItemGroup(ItemGroup itemGroup)
        {
            return await _repo.DeleteEntity(itemGroup);
        }

        public async Task<ItemGroup> GetItemGroup(int id)
        {
            var ItemGroup = await _repo.GetItemGroupById(id);

            return ItemGroup;
        }

        public async Task<IEnumerable<ItemGroup>> GetItemGroups()
        {
            return await _repo.GetItemGroups();
        }

        public async Task<ItemGroup> PostItemGroup(ItemGroup itemGroup)
        {
            var exExist = await _repo.ItemGroupExists(itemGroup.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(itemGroup.GetType().Name, itemGroup.Name);
            }
            else
            {
                return await _repo.AddEntity(itemGroup);
            }
        }

        public async Task<ItemGroup> PutItemGroup(int id, ItemGroup itemGroup)
        {
            if (itemGroup.Id != id)
            {
                throw new ArgumentException($"{nameof(PutItemGroup)}: id in url is not equal to ItemGroup id.");
            }

            return await _repo.PutEntity(itemGroup);
        }

        public ICollection<Item> GenerateItems(ItemGroup itemGroup, int timeSpent, ICollection<object> buffs)
        {
            int actualSpeed = itemGroup.Speed - buffs.Count() * 5;
            actualSpeed = actualSpeed > 5 ? actualSpeed : 5;

            int resourceAmount = timeSpent / actualSpeed;

            ICollection<Item> generatedItems = new List<Item>();
            for (int i = 0; i < resourceAmount; i++)
            {
                generatedItems.Add(itemGroup.GenerateItem());
            }
            return generatedItems;
        }
    }
}
