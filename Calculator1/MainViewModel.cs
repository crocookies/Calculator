using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel model = null;

        public string InputNum
        {
            get { return model.inputNum; }
            set
            {
                model.inputNum = value;
                OnPropertyChanged(nameof(InputNum));
            }
        }

        public string ResultNum
        {
            get { return model.resultNum; }
            set
            {
                model.resultNum = value;
                OnPropertyChanged(nameof(ResultNum));
            }
        }

        public string ShowNum
        {
            get { return model.showNum; }
            set
            {
                model.showNum = value;
                OnPropertyChanged(nameof(ShowNum));
            }
        }

        public string LogNum1
        {
            get { return model.logNum1; }
            set
            {
                model.logNum1 = value;
                OnPropertyChanged(nameof(LogNum1));
            }
        }

        public string LogNum2
        {
            get { return model.logNum2; }
            set
            {
                model.logNum2 = value;
                OnPropertyChanged(nameof(LogNum2));
            }
        }

        public string ShowLogNum
        {
            get { return model.showLogNum; }
            set
            {
                model.showLogNum = value;
                OnPropertyChanged(nameof(ShowLogNum));
            }
        }

        public string CalculateType
        {
            get { return model.calculateType; }
            set
            {
                model.calculateType = value;
                OnPropertyChanged(nameof(CalculateType));
            }
        }

        public bool ShowResult
        {
            get { return model.showResult; }
            set
            {
                model.showResult = value;
                OnPropertyChanged(nameof(ShowResult));
            }
        }

        public ICommand number0 { get; set; }
        public ICommand number1 { get; set; }
        public ICommand number2 { get; set; }
        public ICommand number3 { get; set; }
        public ICommand number4 { get; set; }
        public ICommand number5 { get; set; }
        public ICommand number6 { get; set; }
        public ICommand number7 { get; set; }
        public ICommand number8 { get; set; }
        public ICommand number9 { get; set; }
        public ICommand addition { get; set; }
        public ICommand subtraction { get; set; }
        public ICommand multiplication { get; set; }
        public ICommand division { get; set; }
        public ICommand result { get; set; }
        public ICommand clear { get; set; }
        public ICommand clearEntry { get; set; }
        public ICommand backspace { get; set; }
        public ICommand decimalPoint { get; set; }
        public ICommand reverseNumber { get; set; }
        public ICommand square { get; set; }
        public ICommand root { get; set; }
        public ICommand reciprocal { get; set; }
        public ICommand percent { get; set; }

        public MainViewModel()
        {
            Labels = new ObservableCollection<LabelModel>();

            model = new MainModel();

            number0 = new RelayCommand(ButtonNumber0);
            number1 = new RelayCommand(ButtonNumber1);
            number2 = new RelayCommand(ButtonNumber2);
            number3 = new RelayCommand(ButtonNumber3);
            number4 = new RelayCommand(ButtonNumber4);
            number5 = new RelayCommand(ButtonNumber5);
            number6 = new RelayCommand(ButtonNumber6);
            number7 = new RelayCommand(ButtonNumber7);
            number8 = new RelayCommand(ButtonNumber8);
            number9 = new RelayCommand(ButtonNumber9);
            addition = new RelayCommand(ButtonAddition);
            subtraction = new RelayCommand(ButtonSubtraction);
            multiplication = new RelayCommand(ButtonMultiplication);
            division = new RelayCommand(ButtonDivision);
            result = new RelayCommand(ButtonResult);
            clear = new RelayCommand(ButtonClear);
            clearEntry = new RelayCommand(ButtonClearEntry);
            backspace = new RelayCommand(ButtonBackspace);
            decimalPoint = new RelayCommand(ButtonDecimalPoint);
            reverseNumber = new RelayCommand(ButtonReverseNumber);
            square = new RelayCommand(ButtonSquare);
            root = new RelayCommand(ButtonRoot);
            reciprocal = new RelayCommand(ButtonReciprocal);
            percent = new RelayCommand(ButtonPercent);
        }

        public ObservableCollection<LabelModel> Labels { get; set; }

        // 클리어
        private void Clear()
        {
            InputNum = "0";
            ResultNum = "0";
            ShowNum = "0";
            LogNum1 = "";
            LogNum2 = "";
            ShowLogNum = "";
            CalculateType = "";
            ShowResult = false;
        }

        // 클리어 엔트리
        private void ClearEntry()
        {
            if (ShowNum == ResultNum)
            {
                Clear();
            }
            else
            {
                InputNum = "0";
                ShowNum = InputNum;
            }
        }

        // 백스페이스
        private void Backspace()
        {
            InputNum = InputNum.Substring(0, InputNum.Length - 1);

            if (InputNum == "")
                InputNum = "0";

            ShowNum = InputNum;
        }

        // 숫자 입력하기
        private void InputNumber(int num)
        {
            if (CalculateType == "=")
            {
                Clear();
            }

            if (InputNum == "0")
            {
                InputNum = "";
                InputNum += num.ToString();
            }
            else
            {
                InputNum += num.ToString();
            }

            ShowResult = false;
            ShowNum = InputNum;
        }

        // 소수점 찍기
        private void InputDicimalPoint()
        {
            if (!InputNum.Contains("."))
            {
                InputNum += ".";

                ShowResult = false;
                ShowNum = InputNum;
            }
        }

        // 사칙연산 계산하기
        private void Calculate(string type)
        {
            if (!ShowResult)
            {
                ShowResult = true;

                if (InputNum.EndsWith("."))
                {
                    InputNum = InputNum.Substring(0, InputNum.Length - 1);
                }

                float rNum = new float();
                float iNum = new float();

                float.TryParse(model.resultNum, out rNum);
                float.TryParse(model.inputNum, out iNum);

                switch (CalculateType)
                {
                    case "+":
                        rNum += iNum;
                        break;
                    case "-":
                        rNum -= iNum;
                        break;
                    case "*":
                        rNum *= iNum;
                        break;
                    case "/":
                        rNum /= iNum;
                        break;
                    case "":
                        rNum = iNum;
                        break;
                    case "=":
                        break;
                }
                ResultNum = rNum.ToString();
                ShowNum = ResultNum;
            }

            ShowLogNumber();
            InputNum = "0";
            CalculateType = type;
        }

        // 로그 기록
        private void ShowLogNumber()
        {
            if (LogNum1 == "")
            {
                LogNum1 = ResultNum;
            }
            else if (LogNum2 == "" && CalculateType != "=")
            {
                LogNum2 = InputNum;
                ShowLogNum = LogNum1 + CalculateType + LogNum2 + "=" + ResultNum;

                //AddLabel();
                Labels.Add(new LabelModel(ShowLogNum));
                LogNum1 = ResultNum;
                LogNum2 = "";
            }
        }

        private bool GetLog()
        {
            if (ShowNum == ResultNum)
                return true;
            else
                return false;
        }

        private void SetLog(bool b)
        {
            if (b)
            {
                LogNum1 = ShowNum;
            }
        }

        // 양수 음수 바꾸기
        private void ReverseNumber()
        {
            bool b = GetLog();

            if (ShowNum.StartsWith("-"))
            {
                if (ShowNum == ResultNum)
                {
                    ResultNum = ResultNum.Substring(1);
                    ShowNum = ResultNum;
                }
                else
                {
                    InputNum = InputNum.Substring(1);
                    ShowNum = InputNum;
                }
            }
            else
            {
                if (ShowNum == ResultNum)
                {
                    ResultNum = "-" + ResultNum;
                    ShowNum = ResultNum;
                }
                else
                {
                    InputNum = "-" + InputNum;
                    ShowNum = InputNum;
                }
            }

            SetLog(b);
        }

        // 제곱
        private void Square()
        {
            bool b = GetLog();

            float temp;

            if (ShowNum == ResultNum)
            {
                float.TryParse(model.resultNum, out temp);
                temp *= temp;
                ResultNum = temp.ToString();
                ShowNum = ResultNum;
            }
            else
            {
                float.TryParse(model.inputNum, out temp);
                temp *= temp;
                InputNum = temp.ToString();
                ShowNum = InputNum;
            }

            SetLog(b);
        }

        // 제곱근
        private void Root()
        {
            bool b = GetLog();

            float temp;
            double sqrt;

            if (ShowNum == ResultNum)
            {
                float.TryParse(model.resultNum, out temp);
                sqrt = Math.Sqrt(temp);
                ResultNum = sqrt.ToString();
                ShowNum = ResultNum;
            }
            else
            {
                float.TryParse(model.inputNum, out temp);
                sqrt = Math.Sqrt(temp);
                InputNum = sqrt.ToString();
                ShowNum = InputNum;
            }

            SetLog(b);
        }

        // 역수
        private void Reciprocal()
        {
            bool b = GetLog();

            float temp;
            if (ShowNum == ResultNum)
            {
                float.TryParse(model.resultNum, out temp);
                temp = 1 / temp;
                ResultNum = temp.ToString();
                ShowNum = ResultNum;
            }
            else
            {
                float.TryParse(model.inputNum, out temp);
                temp = 1 / temp;
                InputNum = temp.ToString();
                ShowNum = InputNum;
            }

            SetLog(b);
        }

        // 퍼센트
        private void Percent()
        {
            bool b = GetLog();

            float temp;

            if (ResultNum != "0" && InputNum != "0")
            {
                if (ShowNum == ResultNum)
                {
                    float.TryParse(model.resultNum, out temp);
                    temp /= 100;
                    ResultNum = temp.ToString();
                    ShowNum = ResultNum;
                }
                else
                {
                    float.TryParse(model.inputNum, out temp);
                    temp /= 100;
                    InputNum = temp.ToString();
                    ShowNum = InputNum;
                }
            }

            SetLog(b);
        }

        private void ButtonClear(object parameter)
        {
            Clear();
        }
        private void ButtonClearEntry(object parameter)
        {
            ClearEntry();
        }
        private void ButtonBackspace(object parameter)
        {
            Backspace();
        }
        private void ButtonNumber0(object parameter)
        {
            InputNumber(0);
        }
        private void ButtonNumber1(object parameter)
        {
            InputNumber(1);
        }
        private void ButtonNumber2(object parameter)
        {
            InputNumber(2);
        }
        private void ButtonNumber3(object parameter)
        {
            InputNumber(3);
        }
        private void ButtonNumber4(object parameter)
        {
            InputNumber(4);
        }
        private void ButtonNumber5(object parameter)
        {
            InputNumber(5);
        }
        private void ButtonNumber6(object parameter)
        {
            InputNumber(6);
        }
        private void ButtonNumber7(object parameter)
        {
            InputNumber(7);
        }
        private void ButtonNumber8(object parameter)
        {
            InputNumber(8);
        }
        private void ButtonNumber9(object parameter)
        {
            InputNumber(9);
        }
        private void ButtonAddition(object parameter)
        {
            Calculate("+");
        }
        private void ButtonSubtraction(object parameter)
        {
            Calculate("-");
        }
        private void ButtonMultiplication(object parameter)
        {
            Calculate("*");
        }
        private void ButtonDivision(object parameter)
        {
            Calculate("/");
        }
        private void ButtonResult(object parameter)
        {
            Calculate("=");
        }
        private void ButtonDecimalPoint(object parameter)
        {
            InputDicimalPoint();
        }
        private void ButtonReverseNumber(object parameter)
        {
            ReverseNumber();
        }
        private void ButtonSquare(object parameter)
        {
            Square();
        }
        private void ButtonRoot(object parameter)
        {
            Root();
        }
        private void ButtonReciprocal(object parameter)
        {
            Reciprocal();
        }
        private void ButtonPercent(object parameter)
        {
            Percent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
