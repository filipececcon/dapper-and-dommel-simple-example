using System;
using System.Linq;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;


//Troque apenas o namespace para testar com ou sem o Dommel
using Repository.DapperAndDommel; 

namespace Tests
{
    [TestClass]
    public class PlayerTest
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerTest()
        {
            _playerRepository = new PlayerRepository();
            RegisterMappings.Register();
        }


        [TestMethod]

        public void GetAll()
        {
            try
            {
                var result = _playerRepository.GetAll().ToList();

                Assert.IsTrue(result.Any()); // Se a quantidade de registros for maior que 0
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetById()
        {
            try
            {
                var player = _playerRepository.GetById(2);

                Assert.AreEqual(2, player.Id); // Se o id retornado é igual a 2
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Insert()
        {
            try
            {
                var player = new Player
                {
                    Age = 38,
                    Name = "NOVO JOGADOR",
                    TeamId = 2
                };

                _playerRepository.Insert(ref player);

                Assert.IsTrue(player.Id != 0); // Se populou o id quando inserido
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Update()
        {
            try
            {
                var player = _playerRepository.GetById(4);

                player.Name = "ALTERADO";

                Assert.IsTrue(_playerRepository.Update(player)); //Se atualizou o registro
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Delete()
        {
            try
            {
                Assert.IsTrue(_playerRepository.Delete(8)); // Se deletou o registro
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetList()
        {
            try
            {
                var result = _playerRepository.GetList(x => x.Age > 25).ToList();

                Assert.IsTrue(result.Any()); // Se contém algum item na lista
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
