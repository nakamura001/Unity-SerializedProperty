using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects] // 複数のオブジェクトを選択した状態での編集に対応している場合にはこの記述を行う
[CustomEditor(typeof(MyPlayer2))]
public class MyPlayer2Editor : Editor {
    private SerializedProperty damageProp = null;
    private SerializedProperty armorProp = null;
    private SerializedProperty gunProp = null;

    private void OnEnable () {
        // SerializedPropertyの値を取得
	
		// 最初の取得時には何故か null が返って来るので1度適当な値で読む
    	serializedObject.FindProperty ("__dummy__");
	    armorProp = serializedObject.FindProperty ("armor");
        damageProp = serializedObject.FindProperty ("damage");
        gunProp = serializedObject.FindProperty ("gun");
    }
	
    public override void OnInspectorGUI() {
        // serializedPropertyの更新開始
        serializedObject.Update ();
		
        // int型のデータ向けのスライダー(その他にfloat型向けの Slider も有ります)
        EditorGUILayout.IntSlider (damageProp, 0, 100, new GUIContent ("Damage"));
        // 複数選択時に選択したものの値が全て同じ場合にはプログレスバーを表示
        if (!damageProp.hasMultipleDifferentValues)
            ProgressBar (damageProp.intValue / 100.0f, "Damage");

        EditorGUILayout.IntSlider (armorProp, 0, 100, new GUIContent ("Armor"));
        if (!armorProp.hasMultipleDifferentValues)
            ProgressBar (armorProp.intValue / 100.0f, "Armor");

        EditorGUILayout.PropertyField (gunProp, new GUIContent ("Gun Object"));

        // serializedPropertyの更新を適用
        serializedObject.ApplyModifiedProperties ();
    }

    // プログレスバーの描画
    void ProgressBar (float value, string label) {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
        EditorGUI.ProgressBar (rect, value, label);
        EditorGUILayout.Space ();
    }
}
