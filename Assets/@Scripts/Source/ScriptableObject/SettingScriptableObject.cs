using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SettingScriptableObject : ScriptableObject
{
    private const string SettingFolderPath = "Assets/Resources";
    private const string settingFilePath = "Assets/Resources/NgelSetting.asset";

    public AnimationCurve jumpCurve;
    public Color white = Color.white;
    public Color gray = Color.gray;

    [Title("�Ӽ��� ���� Į��")]
    [LabelText("��")]
    public Color elementFireColor;

    [LabelText("��")]
    public Color elementWaterColor;

    [LabelText("����")]
    public Color elementLightningColor;


    [LabelText("��")]
    public Color elementEarthColor;

    [LabelText("����Ʈ ȿ�� ���� �� ǥ�� Į��")]
    public Color agitUpgadeColor;

    [LabelText("�ΰ��� ���� ���� �� ǥ�� Į��")]
    public Color SelectUnitColor;

    [LabelText("�ΰ��� �̼��� ���� �� ǥ�� Į��")]
    public Color NoneSelectUnitColor;

    [LabelText("�ΰ��� �� ���� �� ǥ�� Į��")]
    public Color EnemyColor;

    #region �÷� ��Ģ

    /*
        IVORY          �ƿ����� ���� �� �⺻ ���� ��                    f3ead6      ���̺���
        CREAM          ��ư �������� / ���� / ���� / ä�� ���� ǥ��     ffeca5      ũ��
        YELLOW         ��ȭ ���� > ������ �ڽ� ��                       ffe275      �����
        YELLOW2        ���� / ������ / �ڱ� �ڽ�	                    f0c568      Ȳ���
        ORANGE         ��ȭ ���� / ����	                                ff8b3d      ��Ȳ��
        ORANGE2        ����	                                            ffb77c      ��Ȳ��
        BROWN          ���� > Ȳ��� �� �Ⱥ��϶�	                    a87c20      ����
        BROWN2         ���� / ������                                    685a4a      ����2
        DARK_BROWN     �ƿ����� ���� ��                                 382c1f      ������
        CYAN           ���� / �¸�                                      94FFFF      �ϴû�
        CYAN2          ������ / �¸�                                    bad1f7      Ǫ�� �ϴ�
        BLUE           ���� �߰�                                        4378d8      �Ķ���
        GREEN          ��ȭ Ȯ��                                        248100      ���
        RED            ���� �� ���	������ / ��� /  ����               ffb9b9      ����
        RED2           ��ο� �� ���	������                          762828      ����2
        RED3           ���� ����	��                                  cc4747      ����3
        GRAY           ��� / ��Ȱ��ȭ                                  919191      ȸ��
    */

    #endregion

    [LabelText("�ƿ����� ���� �� �⺻ ���� ��")]
    public Color IVORY;

    [LabelText("��ư �������� / ���� / ���� / ä�� ���� ǥ��")]
    public Color CREAM;

    [LabelText("��ȭ ���� > ������ �ڽ� ��")]
    public Color YELLOW;

    [LabelText("���� / ������ / �ڱ� �ڽ�")]
    public Color YELLOW2;

    [LabelText("��ȭ ���� / ����")]
    public Color ORANGE;

    [LabelText("����")]
    public Color ORANGE2;

    [LabelText("���� - Ȳ��� �� �Ⱥ��϶�")]
    public Color BROWN;

    [LabelText("���� / ������")]
    public Color BROWN2;

    [LabelText("�ƿ����� ���� ��")]
    public Color DARK_BROWN;

    [LabelText("���� / ��")]
    public Color CYAN;

    [LabelText("������ / �¸�")]
    public Color CYAN2;

    [LabelText("���� �߰�")]
    public Color BLUE;

    [LabelText("��ȭ Ȯ��")]
    public Color GREEN;

    [LabelText("���� �� ��� - ������ / ��� /  ����")]
    public Color RED;

    [LabelText("��ο� �� ���	������ ")]
    public Color RED2;

    [LabelText("���� ���� ��")]
    public Color RED3;

    [LabelText("��� / ��Ȱ��ȭ")]
    public Color GRAY;

    [LabelText("��� �÷� / �Ϲ�")]
    public Color ITEM_RARITY1;

    [LabelText("��� �÷� / ���")]
    public Color ITEM_RARITY2;

    [LabelText("��� �÷� / ���")]
    public Color ITEM_RARITY3;

    [LabelText("��� �÷� / ����")]
    public Color ITEM_RARITY4;

    [LabelText("��� �÷� / ����")]
    public Color ITEM_RARITY5;

    [LabelText("��� �÷� / ���")]
    public Color ITEM_RARITY6;

    [Title("���� �ɼ� �⺻ ���� (���� ����) ")]
    [LabelText("�� ���� Ȥ�� AOS")]
    public List<int> ingameOptionAOS;
    [LabelText("IOS")]
    public List<int> ingameOptionIOS;

    [LabelText("����")]
    public List<int> soundOption;

    [LabelText("�׷���")]
    public List<int> graphicOption;

    [LabelText("Ǫ��")]
    public List<int> pushOption;

    private static SettingScriptableObject instance;
    public Color agitDecoPositiveColor;
    public Color agitDecoNegetiveColor;
    public Color agitDecoTestColor;

    public Color bossRaidNormalColor;
    public Color bossRaidHardColor;

    public Color itemRarity1;
    public Color itemRarity2;
    public Color itemRarity3;
    public Color itemRarity4;
    public Color itemRarity5;
    public Color itemRarity6;

    public Color godInventoryGroupActive;
    public Color godInventoryGroupInactive;

    [Title("����")]
    public Color RelicRuneIcon1;
    public Color RelicRuneIcon2;
    public Color RelicRuneIcon3;
    public Color RelicRuneIcon4;
    public Color RelicRuneIcon5;
    public Color RelicRuneIcon6;

    [LabelText("��鿬��")]
    public Material grayScale;
    public Color grayScaleColor;


    [LabelText("�̼� ������")]
    public Color progressColor;
    [LabelText("�̼� �Ϸ�")]
    public Color completeColor;

    [Title("�õ� ���� ���")]
    public Color ignitionNormal;
    public Color ignitionHigher;
    public Color ignitionRare;
    public Color ignitionHero;
    public Color ignitionLegend;
    public Color ignitionAncient;

    public Color collectionActive;
    public Color collectionInActive;

    public Color guildDungeonMainBoss;
    public Color guildDungeonGuardBoss;
    [Title("���� Į��")]
    public Color errorColor = Color.red;

    [Title("������ �÷� ���� �÷�")]
    public Color bestScoreBossType2;
    public Color bestScoreBossType3;

    public Color currentScoreBossType2;
    public Color currentScoreBossType3;

    public int editorResourceLoadDelayFrame = 1;

    public bool useQATool = false;

    public static SettingScriptableObject Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = Resources.Load<SettingScriptableObject>("Setting");
#if UNITY_EDITOR
            if (instance == null)
            {
                if (!AssetDatabase.IsValidFolder(SettingFolderPath))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                instance = AssetDatabase.LoadAssetAtPath<SettingScriptableObject>(settingFilePath);
                if (instance == null)
                {
                    instance = CreateInstance<SettingScriptableObject>();
                    AssetDatabase.CreateAsset(instance, settingFilePath);
                }
            }
#endif
            return instance;
        }
    }


}