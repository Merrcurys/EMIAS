﻿using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using pr10.Model;
using pr10.View;
using pr10.ViewModel.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

namespace pr10.ViewModel
{
    internal class UserAutoViewModel : BindingHelper
    {
        private string _policyNumber;
        private Patients _currentPatient;

        public string PolicyNumber
        {
            get => _policyNumber;
            set
            {
                _policyNumber = value;
                OnPropertyChanged(nameof(PolicyNumber));
            }
        }

        public Patients CurrentPatient
        {
            get => _currentPatient;
            set
            {
                _currentPatient = value;
                OnPropertyChanged(nameof(CurrentPatient));
            }
        }

        public ICommand AuthenticateCommand { get; }

        public UserAutoViewModel()
        {
            AuthenticateCommand = new RelayCommand(Authenticate);
        }

        private void Authenticate()
        {
            try
            {
                var patientData = ApiHelper.GetById(Convert.ToInt64(PolicyNumber), "Patients");
                var CurrentPatient = JsonConvert.DeserializeObject<Patients>(patientData);

                if (CurrentPatient.oms == Convert.ToInt64(PolicyNumber))
                {
                    UserWindow window = new UserWindow();
                    Application.Current.MainWindow.Close();
                    window.Show();
                } else
                {
                    MessageBox.Show("Пользователь не найден", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пользователь не найден", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
