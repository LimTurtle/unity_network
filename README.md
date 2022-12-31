# Unity HTTP 통신
- API : 정보를 요청하면 reply 해주는 웹사이트
- HTTP : 비동기 통신방법
- GET -> 정보 받아오기(수신) / POST -> 정보 전달(송신)
- Unity에서 HTTP 사용 시, 지연 시간 해결을 위해 코루틴 필요

# Unity JSON
- JSON : 데이터를 통신으로 보낼 때 사용하게 되는 직렬화 방법
- JSON을 적용할 public class 위에 ```[System.Serializable]```을 붙이면 해당 클래스 내 정보를 inspector에서 확인 가능
- 직접 만든 함수를 Unity Inspector에서 테스트 해야 하는 경우, ```[ContextMenu("우리에게 보일 내용")]```를 함수 위에 붙인 후, inspector의 객체 내 컴포넌트 설정에서 해당 함수 실행이 가능
- - -
- JSON 파일 생성 방법
```csharp
string jsonData = JsonUtility.ToJson(player, true); //player는 public Data player로 정의되어 있음
                                                    //이 때 Data는 직렬화가 적용된 사용자 정의 클래스
                                                    //또한 true를 붙여주면 사용자가 보기 좋게 출력
string path = Path.Combine(Application.dataPath, "player_data.json"); //path 설정, 절대 경로 사용 또한 가능
File.WriteAllText(path, jsonData); //json 파일 생성
```
- - -
- JSON 파일 로드 방법
```csharp
string path = Path.Combine(Application.dataPath, "player_data.json"); //path 설정
string jsonData = File.ReadAllText(path); //json 파일을 string형태로 저장
player = JsonUtility.FromJson<Data>(jsonData); //json 정보를 객체에 저장
```
