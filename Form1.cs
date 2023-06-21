using System.Data.Odbc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace cs_form_mtn_008_vs2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 確認_Click(object sender, EventArgs e)
        {
            // 必要なクラス
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            bool result = MysqlConnect(myCon, myCommand);
            if (result == false)
            {
                return;
            }


            // コマンドオブジェクトを接続に関係付ける 
            myCommand.Connection = myCon;
            // 社員コード存在チェック用の SQL 作成
            string strQuery = @$"select * from 社員マスタ
                                    where 社員コード = '{this.社員コード.Text}'";

            myCommand.CommandText = strQuery;
            Debug.WriteLine($"DBG:{strQuery}");

            OdbcDataReader myReader = myCommand.ExecuteReader();
            bool check = myReader.Read();
            // 処理区分が新規で、データが存在したらエラー
            if (this.処理区分.SelectedIndex == 0 && check)
            {
                myReader.Close();
                myCon.Close();
                MessageBox.Show($"入力された社員コードは既に登録されています : {this.社員コード.Text}");

                // 再入力が必要なので、フォーカスして選択
                this.社員コード.Focus();
                this.社員コード.SelectAll();
                return;
            }

            // 接続解除
            myCon.Close();

            // 第二会話へ遷移
            this.ヘッド部.Enabled = false;
            this.ボディ部.Enabled = true;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.氏名.Focus();
            this.氏名.SelectAll();

        }

        private bool MysqlConnect(OdbcConnection myCon, OdbcCommand myCommand)
        {
            bool result = true;


            // 接続文字列の作成
            string server = "localhost";
            string database = "lightbox";
            string user = "root";
            string pass = "";
            string strCon = $"Driver={{MySQL ODBC 8.0 Unicode Driver}};SERVER={server};DATABASE={database};UID={user};PWD={pass}";
            Debug.WriteLine($"DBG:{strCon}");

            myCon.ConnectionString = strCon;

            bool functionExit = false;
            try
            {
                // 接続 
                myCon.Open();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"接続エラー : {ex.Message}");
            }
            // 接続エラーの為
            if (functionExit)
            {
                result = false;
            }



            return result;
        }

        private void キャンセル_Click(object sender, EventArgs e)
        {
            // 第一会話(初期)へ遷移
            this.ヘッド部.Enabled = true;
            this.ボディ部.Enabled = false;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.社員コード.Focus();
            this.社員コード.SelectAll();

            // キャンセルなので入力したフィールドのクリア
            this.氏名.Clear();
            this.給与.Clear();
            this.生年月日.Value = DateTime.Now;

        }

        private void 更新_Click(object sender, EventArgs e)
        {
            // メッセージボックスを表示
            DialogResult result = MessageBox.Show(
                "更新してもよろしいですか?",
                "更新確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
            {
                // 何もしない
            }
            else
            {
                // 更新しないので処理を抜ける( No を選択 )
                this.氏名.Focus();
                this.氏名.SelectAll();
                return;
            }


            // 必要なクラス
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            bool result2 = MysqlConnect(myCon, myCommand);
            if (result2 == false)
            {
                return;
            }

            // 更新処理

            // コマンドオブジェクトを接続に関係付ける 
            myCommand.Connection = myCon;
            // 社員コード存在チェック用の SQL 作成
            string strQuery = @$"insert into `社員マスタ` (
	`社員コード` 
	,`氏名` 
	,`給与` 
	,`生年月日` 
)
 values(
	'{this.社員コード.Text}'
	,'{this.氏名.Text}'
	,{this.給与.Text}
	,'{this.生年月日.Value:yyyy/MM/dd}'
)";

            myCommand.CommandText = strQuery;
            Debug.WriteLine($"DBG:{strQuery}");


            bool functionExit = false;
            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"接続エラー : {ex.Message}");
            }
            // 接続エラーの為
            if (functionExit)
            {
                myCon.Close();
                return;
            }


            // 接続解除
            myCon.Close();

            // 第一会話(初期)へ遷移
            this.ヘッド部.Enabled = true;
            this.ボディ部.Enabled = false;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.社員コード.Focus();
            this.社員コード.SelectAll();

            // キャンセルなので入力したフィールドのクリア
            this.社員コード.Clear();
            this.氏名.Clear();
            this.給与.Clear();
            this.生年月日.Value = DateTime.Now;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.処理区分.SelectedIndex = 0;
        }

        // *****************************************
        // ( フォームの KeyPreview : True )
        // *****************************************
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    this.SelectNextControl(this.ActiveControl, false, true, true, true);

                }
                else
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                e.Handled = true;
            }
        }

        // *****************************************
        // 開始時のフォーカス
        // *****************************************
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.社員コード.Focus();
            this.社員コード.SelectAll();
        }

        // *****************************************
        // リアルタイム入力チェック
        // *****************************************
        private void 社員コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.ActiveControl != this.社員コード)
            {
                // 数字チェック
                if (!Regex.IsMatch(this.社員コード.Text, @"^[0-9]+$"))
                {
                    e.Cancel = true;
                    MessageBox.Show("数字を入力してください");
                    this.社員コード.SelectAll();
                    return;
                }

                // 文字列長チェック
                if (this.社員コード.Text.Length > 4)
                {
                    e.Cancel = true;
                    MessageBox.Show("4桁以内で入力してください");
                    this.社員コード.SelectAll();
                    return;
                }
            }
        }

        private void 社員コード_Validated(object sender, EventArgs e)
        {
            if (this.ActiveControl != this.社員コード)
            {
                // 入力チェック終了後の処理
                this.社員コード.Text = $"{Int32.Parse(this.社員コード.Text):0000}";

            }
        }

        // *****************************************
        // タスクバー(ステータスバー)
        // *****************************************
        private void 社員コード_Enter(object sender, EventArgs e)
        {
            this.ユーザへのメッセージ.Text = "社員コードは 0000 フォーマットで４桁以内で数字で入力します。1～3桁では自動的に 0000 フォーマットに変換します";
        }

        private void 社員コード_Leave(object sender, EventArgs e)
        {
            this.ユーザへのメッセージ.Text = "";
        }

        // *****************************************
        // ファンクションキ－
        // ( フォームの KeyPreview : True )
        // *****************************************
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ヘッド部.Enabled)
            {
                if (e.KeyCode == Keys.F4)
                {
                    this.処理区分.Focus();
                    this.処理区分.DroppedDown = true;
                }

                if (e.KeyCode == Keys.F6)
                {
                    Form2 dialog = new Form2();
                    DialogResult result = dialog.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        // フォーカス処理は戻ってから
                        this.社員コード.Focus();
                        this.社員コード.SelectAll();
                    }
                }
            }

        }
    }
}
