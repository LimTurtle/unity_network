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

# HTTP Response
- Unity 에서 보낸 GET / POST 요청에 대해 Django(SMGWinterDevCamp repo 환경)에서 response를 테스트
- urls.py의 path에 경로를 추가하는데, ```path('HTTP_Test/', views.HTTP_Test.as_view(), name='HTTP_Test'),``` 처럼 as_view() 사용해 클래스로 처리
    * 위 처럼 클래스로 path 지정 시, GET / POST 등 response를 자동으로 처리하는 것이 가능
    
    ```python
    from django.http import JsonResponse, HttpResponse
    class HTTP_Test(View):
        def get(self, request):
            data = {
                "name": "lim",
                "age": 10
            }
            return JsonResponse(data)

        def post(self, request):
            return HttpResponse("Post 요청")
    ```
    
    * 주의할 점은 POST 기능을 사용할 때 Forbidden (CSRF token missing.) 403 오류가 발생하는 경우가 생기는데, 이를 해결하기 위해
        
        1\) ```X-CSRFToken```를 지정하거나,
        
        2\) settings.py -> MIDDLEWARE의 csrf를 주석 처리 (단, 이 방법은 보안 상 문제가 많으니, 다른 해결 방법 찾는 것이 좋음)
        
# Unity + Mirror 연동
- Mirror는 Unity에서 Multiplay 기능을 사용하기 위한 Network Solution
- 프로젝트 파일을 빌드할 때, Project Setting에서 창모드로 800x800 크기로 설정 후, Build and Run (이 때, 경로는 루트 기준 ```/Builds/```)
- 기본적인 연동 순서는
    * Empty Object에 Network Manager / Network Manager HUD / Kcp Transport Component 추가 (이 때, Network Manager의 Transport에 Kcp 넣어주기)
    * Player Prefab 만들고 Network Identity / Network Transform Component / Player Script(C#) 추가
        + Player Script 작성 시, 
            ```csharp
            using Mirror;
            ...
            public class PlayerController : NetworkBehaviour
            ...
            void Start()
            {
                ...
                if(!isLocalPlayer) playerCamera.gameObject.SetActive(false); //Local 카메라만 쫓아가도록
            }
            void Update()
            {
                if(!isLocalPlayer) return;
                ... //이동 관련
            }
            ```
            
    * Network Manager의 Player Prefab에 위에서 만든 Prefab을 넣어주기
    
# Unity WebSocket
- [여기](https://timeboxstory.tistory.com/69)의 C# WebSocket 오픈 소스 사용
- Assets 폴더 내 Plugins 폴더를 생성 후, WebSocket dll파일을 등록하고, ```using WebSocketSharp```으로 사용
- OnXXX 메소드 사용법은 [여기](https://github.com/sta/websocket-sharp)를 참고

## WebSocket 연결
```csharp
m_Socket = new WebSocketSharp.WebSocket("ws://IP주소:포트번호/서비스(웹 url)");
m_Socket.OnMessage += Recv; //메시지를 받은 경우 호출할 Recv(~) 함수
m_Socket.OnClose += CloseConnect; //클라이언트 종료 시 호출할 CloseConnect(~) 함수 
m_Socket.Connect();

```

## WebSocketServer로 데이터 전송
```csharp
m_Socket.Send(Encoding.UTF8.GetBytes(msg)); //같은 C#을 사용하는 서버에서는 Bytes 이용하니 Error. String은 제대로 인식함 -> Why?
```

## WebSocket의 OnXXX 메소드에 의해 호출되는 함수
```csharp
public void Recv(object sender, MessageEventArgs e)
{
    Debug.Log(e.Data);
}

public void CloseConnect(object sender, CloseEventArgs e)
{
    m_Socket.Close();
}
```

## Client가 강제 종료되는 경우
```csharp
private void OnApplicationQuit()
{
    m_Socket.Close(); //강제 종료되더라도, 안전하게 서버와의 통신을 중단
}
```
