using AppSurpervisor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppSurpervisor.Services;

namespace AppSurpervisor.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }
        public PageCadastrar(Funcionario func)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = func.Id.ToString();
            txtNome.Text = func.Nome;
            int setorIndex = pckSetor.Items.IndexOf(func.Setor);
            pckSetor.SelectedIndex = setorIndex;
            int turnoIndex = pckTurno.Items.IndexOf(func.Turno);
            pckTurno.SelectedIndex = turnoIndex;

        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Funcionario func = new Funcionario();
                func.Nome = txtNome.Text;
                func.Setor = pckSetor.SelectedItem.ToString();
                func.Turno = pckTurno.SelectedItem.ToString();
                ServiceDbFuncionario dbNotas = new ServiceDbFuncionario(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dbNotas.Inserir(func);
                    DisplayAlert("Resultado", dbNotas.StatusMessage, "OK");
                }
                else
                {
                    func.Id = Convert.ToInt32(txtCodigo.Text);
                    dbNotas.Alterar(func);
                    DisplayAlert("Nota Alterada com Sucesso!", dbNotas.StatusMessage, "OK");
                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Funcionário", "Deseja excluir o funcionário selecionada?", "Sim", "Não");
            if (resp == true)
            {
                ServiceDbFuncionario dbNotas = new ServiceDbFuncionario(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dbNotas.Excluir(id);
                DisplayAlert("Nota excluída com Sucesso!", dbNotas.StatusMessage, "OK");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}