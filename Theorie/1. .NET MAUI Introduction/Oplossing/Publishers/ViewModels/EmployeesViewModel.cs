using Publishers.Data.Interface;
using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.ViewModels
{
    public partial class EmployeesViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Employee> employees;

        [ObservableProperty]
        public ObservableCollection<Job> jobs;

        [ObservableProperty]
        public ObservableCollection<Publisher> publishers;

        [ObservableProperty]
        public Job selectedJob;

        [ObservableProperty]
        public Publisher selectedPublisher;

        [ObservableProperty]
        public string id;

        [ObservableProperty]
        public DateTime selectedDate = DateTime.Now;

        private IEmployeesRepository _employeesRepository;
        private IJobsRepository _jobsRepository;
        private IPublishersRepository _publishersRepository;

        public EmployeesViewModel(EmployeesRepository employeesRepository, JobsRepository jobsRepository, PublishersRepository publishersRepository)
        {
            _employeesRepository = employeesRepository;
            _jobsRepository = jobsRepository;
            _publishersRepository = publishersRepository;

            Jobs = new ObservableCollection<Job>(_jobsRepository.OphalenJobs());
            Publishers = new ObservableCollection<Publisher>(_publishersRepository.OphalenPublishers());
        }

        [RelayCommand]
        public void AlleWerknemersOphalen()
        {
            IsBusy = true;
            Employees = new ObservableCollection<Employee>(_employeesRepository.OphalenEmployees());
            IsBusy = false;
        }

        [RelayCommand]
        public void WerknemersOphalenViaJob()
        {
            if (SelectedJob == null)
            {
                Shell.Current.DisplayAlert("Error", "Please select a job.", "Ok");
                return;
            }

            IsBusy = true;
            Employees = new ObservableCollection<Employee>(_employeesRepository.OphalenEmployeesViaJobId(SelectedJob.Id));
            IsBusy = false;
        }

        [RelayCommand]
        public void WerknemersOphalenViaPublisher()
        {
            if (SelectedPublisher == null)
            {
                Shell.Current.DisplayAlert("Error", "Please select a publisher.", "Ok");
                return;
            }

            IsBusy = true;
            Employees = new ObservableCollection<Employee>(_employeesRepository.OphalenEmployeesViaPublisherId(SelectedPublisher.Id));
            IsBusy = false;
        }

        [RelayCommand]
        public void WerknemersOphalenViaJobEnPublisher()
        {
            if (SelectedJob == null)
            {
                Shell.Current.DisplayAlert("Error", "Please select a job.", "Ok");
                return;
            }

            if (SelectedPublisher == null)
            {
                Shell.Current.DisplayAlert("Error", "Please select a publisher.", "Ok");
                return;
            }

            IsBusy = true;
            Employees = new ObservableCollection<Employee>(_employeesRepository.OphalenEmployeesViaPublisheridEnJobid(SelectedPublisher.Id, SelectedJob.Id));
            IsBusy = false;
        }

        [RelayCommand]
        public void WerknemersOphalenViaAanwerfdatum()
        {
            IsBusy = true;
            Employees = new ObservableCollection<Employee>(_employeesRepository.OphalenEmployeesViaHireDate(SelectedDate));
            IsBusy = false;
        }

        [RelayCommand]
        public void WerknemerOphalenViaId()
        {
            if (!int.TryParse(Id, out int id))
            {
                Shell.Current.DisplayAlert("Fout", "Geef een geldige ID.", "Sluiten");
                return;
            }
            IsBusy = true;
            var employee = _employeesRepository.OphalenEmployeeViaId(id);
            if (employee == null)
                Shell.Current.DisplayAlert("Fout", $"Employee met ID {id} werd niet gevonden.", "Sluiten");
            else
                Shell.Current.DisplayAlert("Employee gevonden", employee.FirstName, "Sluiten");

            IsBusy = false;
        }
    }
}