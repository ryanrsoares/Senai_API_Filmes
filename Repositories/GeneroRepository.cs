using api_filmes_senai.Context;
using api_filmes_senai.Domains;
using api_filmes_senai.Interface;

namespace api_filmes_senai.Repositories
{
    /// <summary>
    /// Classe que vai implementar a interface IGeneroRepository
    /// Ou seja. vamos umplementar os métodos, toda a lógica dos métodos
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {

        /// <summary>
        /// Variável privada e somente leitura 
        /// que "guarda" os dados no contexto
        /// </summary>
        private readonly Filmes_Context _context;
        private Genero generoBuscado;

        /// <summary>
        /// Construtor do repositorio
        /// Aqui, todas vez que o construtor for chamado,
        /// os dados do contexro estarão disponíveis
        /// </summary>
        /// <param name="contexto">Dados do contexto</param>
        public GeneroRepository(Filmes_Context contexto)
        {
            _context = contexto;
        }

        public void Atualizar(Guid id, Genero genero)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = genero.Nome;
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Genero BuscarPorId(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;

                return generoBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Método para cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero">objeto gênero a ser cadastrado</param>
        public void Cadastrar(Genero novoGenero)
        {
            try
            {
                //Adiciona um novo gênero a tablea Generos(BD = banco de dados)
                _context.Genero.Add(novoGenero);

                //Após o cadastro, salva as alterações (BD)
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //ctrl + k + c : atalho para comentar o código
        //ctrl + k + d : atalho para identar o código
        public void Deletar(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;
                if (generoBuscado != null)
                {
                    _context.Genero.Remove(generoBuscado);
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Genero> Listar()
        {
            try
            {
                List<Genero> listaGeneros = _context.Genero.ToList();

                return listaGeneros;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
