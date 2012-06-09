#pragma strict
@CustomEditor(MyPlayer)
@CanEditMultipleObjects // 複数のオブジェクトを選択した状態での編集に対応している場合にはこの記述を行う
class MyPlayerEditor extends Editor {
    var damageProp : SerializedProperty;
    var armorProp : SerializedProperty;
    var gunProp : SerializedProperty;

    function OnEnable () {
        // SerializedPropertyの値を取得
        damageProp = serializedObject.FindProperty ("damage");
        armorProp = serializedObject.FindProperty ("armor");
        gunProp = serializedObject.FindProperty ("gun");
    }

    function OnInspectorGUI() {
        // serializedPropertyの更新開始
        serializedObject.Update ();

        // int型のデータ向けのスライダー(その他にfloat型向けの Slider も有ります)
        EditorGUILayout.IntSlider (damageProp, 0, 100, new GUIContent ("Damage"));
        // 複数選択時に選択したものの値が全て同じ場合にはプログレスバーを表示
        if (!damageProp.hasMultipleDifferentValues)
            ProgressBar (damageProp.intValue / 100.0, "Damage");

        EditorGUILayout.IntSlider (armorProp, 0, 100, new GUIContent ("Armor"));
        if (!armorProp.hasMultipleDifferentValues)
            ProgressBar (armorProp.intValue / 100.0, "Armor");

        EditorGUILayout.PropertyField (gunProp, new GUIContent ("Gun Object"));

        // serializedPropertyの更新を適用
        serializedObject.ApplyModifiedProperties ();
    }

    // プログレスバーの描画
    function ProgressBar (value : float, label : String) {
        // Get a rect for the progress bar using the same margins as a textfield:
        var rect : Rect = GUILayoutUtility.GetRect (18, 18, "TextField");
        EditorGUI.ProgressBar (rect, value, label);
        EditorGUILayout.Space ();
    }
}
