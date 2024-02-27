using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppSurpervisor.View;
using AppSurpervisor.Services;
using AppSurpervisor.Models;
using System.Numerics;

namespace AppSurpervisor.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            atualizaLista();
        }
        public void atualizaLista()
        {
            string titulo = "";
            if (txtNome.Text != null) titulo = txtNome.Text;
            ServiceDbFuncionario dbFunc = new ServiceDbFuncionario(App.DbPath);
            ListaFunc.ItemsSource = dbFunc.Localizar(titulo);
        }        

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            atualizaLista();
        }

        private void ListaFunc_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Funcionario func = (Funcionario)ListaFunc.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(func));
        }
        
    }
}