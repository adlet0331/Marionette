# About Project

## About Unity

[유니티 문서](https://docs.unity3d.com/kr/current/Manual/UnityManual.html)

### 1. Unity Core

[Unity Overview](https://docs.unity3d.com/kr/current/Manual/UnityOverview.html)

[Unity UI](https://docs.unity3d.com/kr/current/Manual/com.unity.ugui.html)

[Unity NavMesh For Enemy AI](https://docs.unity3d.com/kr/current/Manual/nav-NavigationSystem.html)

#### Editor

[About Editor](https://docs.unity3d.com/kr/current/Manual/UsingTheEditor.html)

#### System

Prefab
- What is Prefab?
- Why use Prefab? 
  - Version Control Advantage

Unity Physics
- Box Collider + Rigidbody
- What is Is Trigger? 

### 2. Unity Graphics

[Animation](https://docs.unity3d.com/kr/current/Manual/AnimationSection.html)

[Material](https://docs.unity3d.com/kr/current/Manual/Materials.html)

Universial Render Pipeline (Unity Package)

[URP Manual](https://docs.unity3d.com/kr/Packages/com.unity.render-pipelines.universal@8.2/manual/index.html)

Shader Graph
- CS380 컴퓨터그래픽스개론 내용
- 현재 이 프로젝트는 위에서 바닥에 놓인 종이 인형에 빛을 비추는 것과 같음
- Shader도 2D 픽셀만 고려하면 됨 (No Normal Vector)

How To Use?
- Light Object
- Shadow Map
  - Light 2D Object

### 3. Scripting 

#### Rider - Good IDLE
- Setting Rider For Unity
- Keymap
- Debugging

#### Unity Scripting

- Monobehavior, [Unity Lifecycle](https://docs.unity3d.com/kr/2021.3/Manual/ExecutionOrder.html)
- [Serialize](https://docs.unity3d.com/kr/2018.4/Manual/script-Serialization.html)
- OnCollision (유니티 물리)
- How to Apply My Script To Unity Objects?

## 협업

### Git - Source Version Control

Gitlab Project Access Tokens

Git Basic Commands (In Fork)
- git pull
- git commit
- git push
- git tag (For CI)

Local vs Remote

.gitignore

### CI - Continuous Integration

거창한건 아니고.. 빌드 자주 뽑기. (안 터지는 거 확인)

Project Link: https://git.haje.org/Adlet/doll-project

Pipeline

.gitlab-ci.yml
