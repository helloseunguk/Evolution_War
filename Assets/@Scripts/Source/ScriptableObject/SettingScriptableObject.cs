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

    [Title("속성별 쉴드 칼라")]
    [LabelText("불")]
    public Color elementFireColor;

    [LabelText("물")]
    public Color elementWaterColor;

    [LabelText("번개")]
    public Color elementLightningColor;


    [LabelText("땅")]
    public Color elementEarthColor;

    [LabelText("아지트 효과 증가 시 표현 칼라")]
    public Color agitUpgadeColor;

    [LabelText("인게임 선택 유닛 원 표시 칼라")]
    public Color SelectUnitColor;

    [LabelText("인게임 미선택 유닛 원 표시 칼라")]
    public Color NoneSelectUnitColor;

    [LabelText("인게임 적 유닛 원 표시 칼라")]
    public Color EnemyColor;

    #region 컬러 규칙

    /*
        IVORY          아웃라인 있을 때 기본 글자 색                    f3ead6      아이보리
        CREAM          버튼 켜졌을때 / 레벨 / 선택 / 채팅 본인 표시     ffeca5      크림
        YELLOW         강화 숫자 > 아이템 박스 안                       ffe275      노란색
        YELLOW2        강조 / 긍정적 / 자기 자신	                    f0c568      황토색
        ORANGE         강화 숫자 / 강조	                                ff8b3d      주황색
        ORANGE2        강조	                                            ffb77c      주황색
        BROWN          강조 > 황토색 잘 안보일때	                    a87c20      갈색
        BROWN2         본문 / 부제목                                    685a4a      갈색2
        DARK_BROWN     아웃라인 없을 때                                 382c1f      진갈색
        CYAN           승점 / 승리                                      94FFFF      하늘색
        CYAN2          긍정적 / 승리                                    bad1f7      푸른 하늘
        BLUE           점수 추가                                        4378d8      파란색
        GREEN          강화 확률                                        248100      녹색
        RED            밝은 곳 사용	부정적 / 경고 /  패점               ffb9b9      빨강
        RED2           어두운 곳 사용	부정적                          762828      빨강2
        RED3           강조 버전	패                                  cc4747      빨강3
        GRAY           잠금 / 비활성화                                  919191      회색
    */

    #endregion

    [LabelText("아웃라인 있을 때 기본 글자 색")]
    public Color IVORY;

    [LabelText("버튼 켜졌을때 / 레벨 / 선택 / 채팅 본인 표시")]
    public Color CREAM;

    [LabelText("강화 숫자 > 아이템 박스 안")]
    public Color YELLOW;

    [LabelText("강조 / 긍정적 / 자기 자신")]
    public Color YELLOW2;

    [LabelText("강화 숫자 / 강조")]
    public Color ORANGE;

    [LabelText("강조")]
    public Color ORANGE2;

    [LabelText("강조 - 황토색 잘 안보일때")]
    public Color BROWN;

    [LabelText("본문 / 부제목")]
    public Color BROWN2;

    [LabelText("아웃라인 없을 때")]
    public Color DARK_BROWN;

    [LabelText("승점 / 승")]
    public Color CYAN;

    [LabelText("긍정적 / 승리")]
    public Color CYAN2;

    [LabelText("점수 추가")]
    public Color BLUE;

    [LabelText("강화 확률")]
    public Color GREEN;

    [LabelText("밝은 곳 사용 - 부정적 / 경고 /  패점")]
    public Color RED;

    [LabelText("어두운 곳 사용	부정적 ")]
    public Color RED2;

    [LabelText("강조 버전 패")]
    public Color RED3;

    [LabelText("잠금 / 비활성화")]
    public Color GRAY;

    [LabelText("등급 컬러 / 일반")]
    public Color ITEM_RARITY1;

    [LabelText("등급 컬러 / 고급")]
    public Color ITEM_RARITY2;

    [LabelText("등급 컬러 / 희귀")]
    public Color ITEM_RARITY3;

    [LabelText("등급 컬러 / 영웅")]
    public Color ITEM_RARITY4;

    [LabelText("등급 컬러 / 전설")]
    public Color ITEM_RARITY5;

    [LabelText("등급 컬러 / 고대")]
    public Color ITEM_RARITY6;

    [Title("게임 옵션 기본 설정 (최초 실행) ")]
    [LabelText("인 게임 혹은 AOS")]
    public List<int> ingameOptionAOS;
    [LabelText("IOS")]
    public List<int> ingameOptionIOS;

    [LabelText("사운드")]
    public List<int> soundOption;

    [LabelText("그래픽")]
    public List<int> graphicOption;

    [LabelText("푸시")]
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

    [Title("유물")]
    public Color RelicRuneIcon1;
    public Color RelicRuneIcon2;
    public Color RelicRuneIcon3;
    public Color RelicRuneIcon4;
    public Color RelicRuneIcon5;
    public Color RelicRuneIcon6;

    [LabelText("흑백연출")]
    public Material grayScale;
    public Color grayScaleColor;


    [LabelText("미션 진행중")]
    public Color progressColor;
    [LabelText("미션 완료")]
    public Color completeColor;

    [Title("시동 무기 배경")]
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
    [Title("에러 칼라")]
    public Color errorColor = Color.red;

    [Title("관리자 시련 보스 컬러")]
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