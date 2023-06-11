# cs-form-mtn-008-vs2022

### 一覧( DataGridView ) による更新処理

![image](https://github.com/winofsql/cs-form-mtn-008-vs2022/assets/1501327/a0ece432-e13e-4384-b3c9-492a6886919c

- ### 画面コントロール
  - 更新ボタンの初期設定は Enabled = false
  - SQL は order by で 主キー順に並べる
  - データがあった場合のみ更新ボタンを使用可にする
  - DataGridView は編集のみ可とする
  - DataGridView の複数選択は不可とする
  - DataGridView の行選択は全ての列とする
  - ESC キーでフォームを閉じて、Form1 の社員コードに現在の最大値 + 1 を編集したものをセットする
  - セット可能にする為に社員コードの modifiers を internal とする
