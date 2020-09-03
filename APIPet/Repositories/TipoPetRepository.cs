﻿using APIPet.Context;
using APIPet.Domains;
using APIPet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIPet.Repositories
{
    public class TipoPetRepository : ITipoPet
    {
        TipoPetContext conexao = new TipoPetContext();

        SqlCommand cmd = new SqlCommand();
        public TipoPet Alterar(int id, TipoPet pet)
        {
            cmd.Connection = conexao.Conectar();
            cmd.CommandText = "UPDATE TipoPet SET " +
                "Descricao = @Descricao," +
                "DataDeNascimento = @DataDeNascimento WHERE IdTipoPet = @id";

            cmd.Parameters.AddWithValue("id", id);

            cmd.Parameters.AddWithValue("@Descricao", pet.Descricao);
            cmd.Parameters.AddWithValue("@DataDeNascimento", pet.DataDeNascimento);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();
            return pet;
        }

        public TipoPet BuscarPorID(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM TipoPet WHERE IdTipoPet = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            TipoPet pet = new TipoPet();

            while (dados.Read())
            {

                pet.IdTipoPet = Convert.ToInt32(dados.GetValue(0));
                pet.Descricao = dados.GetValue(1).ToString();
                pet.DataDeNascimento = Convert.ToDateTime(dados.GetValue(2));
            }

            conexao.Desconectar();

            return pet;
        }

        public TipoPet Cadastrar(TipoPet pet)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO TipoPet (Descricao, DataDeNascimento) " +
                "VALUES" +
                "(@Descricao, @DataDeNascimento)";
            cmd.Parameters.AddWithValue("@Descricao", pet.Descricao);
            cmd.Parameters.AddWithValue("@DataDeNascimento", pet.DataDeNascimento);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return pet;
        }

        public void Excluir(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Aluno WHERE IdTipoPet = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();
        }

        public List<TipoPet> ListarTodos()
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM TipoPet";

            SqlDataReader dados = cmd.ExecuteReader();

            List<TipoPet> pets = new List<TipoPet>();

            while (dados.Read())
            {
                pets.Add(
                    new TipoPet()
                    {
                        IdTipoPet = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString(),
                        DataDeNascimento = Convert.ToDateTime(dados.GetValue(2)),
                    }
                );
            }

            conexao.Desconectar();

            return pets;
        }
    }
}
