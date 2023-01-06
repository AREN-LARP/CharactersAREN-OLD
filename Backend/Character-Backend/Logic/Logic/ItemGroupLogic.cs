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
        public async Task<ItemGroup> GetItemGroupBySkill(Skill skill)
        {
            var itemGroups = await _repo.GetItemGroupsBySkillId(skill.Id);
            return itemGroups.First();
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

        public ICollection<Tuple<Item, int>> GenerateItems(ItemGroup itemGroup, int timeSpent, ICollection<Skill> buffs)
        {
            int actualSpeed = itemGroup.Speed - buffs.Count * 5;
            actualSpeed = actualSpeed > 5 ? actualSpeed : 5;

            int resourceAmount = timeSpent / actualSpeed;

            ICollection<Tuple<Item, int>> generatedItems = new List<Tuple<Item, int>>();
            for (int i = 0; i < resourceAmount; i++)
            {
                Item it = itemGroup.GenerateItem();
                Tuple<Item, int> tuple = generatedItems.FirstOrDefault(t => t.Item1 == it, new Tuple<Item, int>(it, 0));
                generatedItems.Remove(tuple);
                tuple = new Tuple<Item, int>(tuple.Item1, tuple.Item2 + 1);
                generatedItems.Add(tuple);
            }

            return generatedItems;
        }
    }
}
