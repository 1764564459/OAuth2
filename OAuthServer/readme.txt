
Guthub 一下库
1、Microsoft.AspNet.Identity.EntityFramework
2、Owin 可添加 startup文件
3、Microsoft.AspNet.Identity.Owin
4、OwinHost 不太确定
5、Microsoft.Owin.Host.SystemWeb
6、安装jwt --> Microsoft.Owin.Security.Jwt

实现获取code
 1、补全startup文件
  2、新增Model下面Server Provider文件（实现-->授权服务器对Client的验证-->授权码提供器-->刷新Token提供器）
  

postman 获取Access Token

1、Authorization_code
获取Access Token地址：http://localhost:60726/Auth/token
client_id --> id 
grant_type --> authorization_code 
code --> code
redirect_url --> http://localhost:60726/

获取到AccessToken 并访问资源


2、refresh token

获取Access Token地址：http://localhost:60726/Auth/token
client_id --> id 
grant_type --> refresh_token 
refresh_token -->  refreshToken  value

3、password

获取Access Token地址：http://localhost:60726/Auth/token
client_id --> id 
grant_type --> password 
username --> admin
password --> 123456

4、client_credentials

获取Access Token地址：http://localhost:60726/Auth/token
client_id --> id 
client_secret --> 123456789 （client类） 
grant_type --> client_credentials

