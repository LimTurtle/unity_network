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
        

    
