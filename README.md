# Disaster — Debugging Lab

## English

### 1. Get the project
Clone the repository or download the ZIP and unzip it. In **Unity Hub**, choose **Add → Add project from disk** and select the folder. If Hub asks about the Unity version, choose the closest installed version and confirm the upgrade.

### 2. Opening with errors
Unity will tell you the project has compilation errors and offer **Safe Mode**.
- **Enter Safe Mode**: the Editor opens so you can fix the scripts first.
- **Ignore**: the Editor opens normally, but Play is blocked until the errors are fixed.

Either choice is fine.

### 3. Fix the compiler errors first
1. Open the Console (**Window → General → Console**).
2. Read the first red error: file name, line number, message.
3. Double-click it to open the script, fix it, save, then go back to Unity and wait for it to recompile.
4. Repeat until the Console has no red errors. New errors can appear after you fix one; that is normal.

### 4. Open the scene
Open `Assets/Scenes/Disaster`. If some components say "script can not be loaded", there is still a script problem to fix.

### 5. Press Play and fix the runtime problems
- Use the controls shown on the screen.
- The Console will be very noisy at first. Find out why.
- **Save before pressing 5.**

### 6. The debugging loop
**Reproduce → Isolate → Inspect → Fix → Confirm.** Change one thing at a time.

### 7. Debugger setup
1. Set the External Script Editor (Visual Studio, VS Code with the Unity extension, or Rider) in **Edit → Preferences → External Tools**.
2. Switch Unity to **Debug mode** (bug icon, bottom-right of the Editor).
3. Attach to Unity, set a breakpoint, press Play, then use Step Over / Step Into and read the Locals.

### 8. Done when
Play runs with an empty Console (except your own logs), the player moves and jumps, the door opens, the lamp toggles, both goals log, and the score goes up when you collect coins.

---

## 日本語

### 1. プロジェクトを入手する
リポジトリをクローンするか、ZIP をダウンロードして展開します。**Unity Hub** で **Add → Add project from disk** を選び、フォルダーを指定します。Unity のバージョンについて聞かれたら、インストール済みで最も近いバージョンを選び、アップグレードを確認します。

### 2. エラーがある状態で開く
Unity が「プロジェクトにコンパイルエラーがある」と表示し、**Safe Mode**（セーフモード）を提案します。
- **Enter Safe Mode**：エディターが開くので、先にスクリプトを修正します。
- **Ignore**：エディターは通常どおり開きますが、エラーを直すまで Play はできません。

どちらを選んでも構いません。

### 3. まずコンパイルエラーを直す
1. Console を開きます（**Window → General → Console**）。
2. 最初の赤いエラーを読みます：ファイル名、行番号、メッセージ。
3. ダブルクリックしてスクリプトを開き、修正して保存し、Unity に戻って再コンパイルを待ちます。
4. Console に赤いエラーがなくなるまで繰り返します。1 つ直すと新しいエラーが出ることがありますが、正常です。

### 4. シーンを開く
`Assets/Scenes/Disaster` を開きます。「script can not be loaded」と表示されるコンポーネントがあれば、まだスクリプトに問題があります。

### 5. Play して実行時の問題を直す
- 画面に表示されている操作方法を使います。
- 最初は Console がとても騒がしくなります。その理由を調べましょう。
- **5 を押す前に保存してください。**

### 6. デバッグの流れ
**再現 → 切り分け → 調査 → 修正 → 確認。** 一度に変更するのは 1 つだけにします。

### 7. デバッガーの設定
1. **Edit → Preferences → External Tools** で External Script Editor（Visual Studio、Unity 拡張機能付き VS Code、または Rider）を設定します。
2. Unity を **Debug モード**にします（エディター右下の虫のアイコン）。
3. Unity にアタッチし、ブレークポイントを設定して Play し、ステップオーバー／ステップインで Locals を確認します。

### 8. 完了の条件
Console が空（自分で入れたログを除く）の状態で Play でき、プレイヤーが移動・ジャンプし、ドアが開き、ランプが切り替わり、両方のゴールがログを出し、コインを取るとスコアが増えれば完了です。
