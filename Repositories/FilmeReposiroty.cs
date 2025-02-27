using api_filmes_senai.Context;
using api_filmes_senai.Domains;
using api_filmes_senai.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_filmes_senai.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly Filmes_Context _context;
        private bool listaDeFilme;

        public FilmeRepository(Filmes_Context context)
        {
            _context = context;
        }


        public void Atualizar(Guid id, Filme filme)
        {
            try
            {
                Filme filmeBuscado = _context.Filme.Find(id)!;

                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filme.Titulo;
                    filmeBuscado.IdGenero = filme.IdGenero;
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Atualizar(Guid id, Genero filme)
        {
            throw new NotImplementedException();
        }

        public Filme BuscarFilmePorId(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filme.Find(id)!;

                return filmeBuscado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Cadastrar(Filme novoFilme)
        {
            try
            {
                _context.Filme.Add(novoFilme);

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filme.Find(id)!;
                if (filmeBuscado != null)
                {
                    _context.Filme.Remove(filmeBuscado);
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Filme> Listar()
        {
            try
            {
                List<Filme> ListaDeFilmes = _context.Filme
                    .Include(g => g.Genero)
                    //Seleciona o que quer trazer na requisição
                    .Select(f => new Filme
                    {
                        //dados de filme
                        IdFilme = f.IdFilme,
                        Titulo = f.Titulo,

                        //dados de genero
                        Genero = new Genero
                        {
                            IdGenero = f.IdGenero,
                            Nome = f.Genero!.Nome
                        }
                    })
                    .ToList();

                return ListaDeFilmes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Listar os filmes pelo seu genero - filtro

        public List<Filme> ListarPorGenero(Guid idGenero)
        {
            try
            {
                List<Filme> listaDeFilmes = _context.Filme

                    .Include(g => g.Genero)
                    .Where(f => f.IdGenero == idGenero)
                    .ToList();

                return listaDeFilmes;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
