using System;
using System.Collections.Generic;
using System.Text;
using AppSurpervisor.Models;
using SQLite;

namespace AppSurpervisor.Services
{
    internal class ServiceDbFuncionario
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServiceDbFuncionario(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Funcionario>();
        }
        public void Inserir(Funcionario f)
        {
            try
            {
                if (string.IsNullOrEmpty(f.Nome))
                    throw new Exception("Nome do funcionário não informado!");
                if (string.IsNullOrEmpty(f.Setor))
                    throw new Exception("Setor do funcionário não informado!");
                if (string.IsNullOrEmpty(f.Turno))
                    throw new Exception("Turno do funcionário não informado!");
                int result = conn.Insert(f);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} Registro adicionado: [Nota: {1}]", result, f.Nome);
                }
                else
                {
                    this.StatusMessage = string.Format("Registro NÃO adicionado! Por favor, informe os dados do Funcionário!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Funcionario> Listar()
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                lista = conn.Table<Funcionario>().ToList();
                this.StatusMessage = "Listagem dos Funcionários";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lista;
        }
        public void Alterar(Funcionario f)
        {
            try
            {
                if (string.IsNullOrEmpty(f.Nome))
                    throw new Exception("Nome do funcionário não informado!");
                if (string.IsNullOrEmpty(f.Setor))
                    throw new Exception("Setor do funcionário não informado!");
                if (string.IsNullOrEmpty(f.Turno))
                    throw new Exception("Turno do funcionário não informado!");
                if (f.Id <= 0)
                    throw new Exception("Id do funcionário não informado");
                int result = conn.Update(f);
                StatusMessage = string.Format("{0} Registro alterado!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<Funcionario>().Delete(r => r.Id == id);
                StatusMessage = string.Format("{0} Registro deletado!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public List<Funcionario> Localizar(string Nome)
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                var resp = from p in conn.Table<Funcionario>() where p.Turno.ToLower().Contains(Nome.ToLower()) select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
        public List<Funcionario> Localizar(string Nome, string Setor)
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                var resp = from p in conn.Table<Funcionario>() where p.Nome.ToLower().Contains(Nome.ToLower()) && p.Setor == Setor select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
        public Funcionario GetFunc(int id)
        {
            Funcionario m = new Funcionario();
            try
            {
                m = conn.Table<Funcionario>().First(n => n.Id == id);
                StatusMessage = "Encontrei o funcionário!";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return m;
        }


    }
}
