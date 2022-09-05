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
    public class ItemLogic : IItemLogic
    {
        private readonly IItemRepository _repo;

        public ItemLogic(IItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<Item> DeleteItem(Item skill)
        {
            return await _repo.DeleteEntity(skill);
        }

        public async Task<Item> GetItem(int id)
        {
            var skill = await _repo.GetItemById(id);

            return skill;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _repo.GetItems();
        }

        public async Task<Item> PostItem(Item skill)
        {
            var exExist = await _repo.ItemExists(skill.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(skill.GetType().Name, skill.Name);
            }
            else
            {
                return await _repo.AddEntity(skill);
            }
        }

        public async Task<Item> PutItem(int id, Item skill)
        {
            if (skill.Id != id)
            {
                throw new ArgumentException($"{nameof(PutItem)}: id in url is not equal to item id.");
            }

            return await _repo.PutEntity(skill);
        }
    }
}
