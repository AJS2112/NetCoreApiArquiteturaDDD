using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Usuario
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserCreateDTO()
            {
                Name = _name,
                Email = _email
            };
            //POST
            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserCreateResultDTO>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, registroPost.Name);
            Assert.Equal(_email, registroPost.Email);
            Assert.False(registroPost.Id == default(Guid));


            //GETALL
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            //PUT
            var updateUserDto = new UserUpdateDTO()
            {
                Id = registroPost.Id,
                Name = Faker.Name.First(),
                Email = Faker.Internet.Email()
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserUpdateResultDTO>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroPost.Name, registroAtualizado.Name);

            //GET(Id)
            response = await client.GetAsync($"{hostApi}users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDTO>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Name, registroAtualizado.Name);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

    }
}