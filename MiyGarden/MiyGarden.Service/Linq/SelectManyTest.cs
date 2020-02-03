using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Linq
{
    public class SelectManyTest
    {
        public IEnumerable<object> GetTestData()
        {
            var query = GetAll().SelectMany(owner => owner.Pets,
                  (owner, pet) => new { Owner = owner.Name, Pet = pet.Name });
            return query;
        }

        private IQueryable<PetOwner> GetAll()
        {
            PetOwner[] petOwners =
                    { new PetOwner { Name="Higa",
              Pets = new List<Pet>{
                  new Pet { Name="Scruffy", Breed="Poodle" },
                  new Pet { Name="Sam", Breed="Hound" } } },
                      new PetOwner { Name="Ashkenazi",
              Pets = new List<Pet>{
                  new Pet { Name="Walker", Breed="Collie" },
                  new Pet { Name="Sugar", Breed="Poodle" } } },
                      new PetOwner { Name="Price",
              Pets = new List<Pet>{
                  new Pet { Name="Scratches", Breed="Dachshund" },
                  new Pet { Name="Diesel", Breed="Collie" } } },
                      new PetOwner { Name="Hines",
              Pets = new List<Pet>{
                  new Pet { Name="Dusty", Breed="Collie" } } }};
            return petOwners.AsQueryable();
        }
    }

    class PetOwner
    {
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public string Breed { get; set; }
    }
}
