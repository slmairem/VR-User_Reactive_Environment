# VR Project: User-Reactive Environment Design 

This project includes several visual and interactive features that make the virtual environment more dynamic and interesting. 

### Skybox and Day/Night Cycle 
The project has a Day and Night system: 
- In the daytime, a Procedural Skybox is used. 
- At night, a Custom Skybox appears with aurora lights and stars.

<img src="https://github.com/user-attachments/assets/e708c6b3-c3f4-4535-bcc6-3d2075c2d099" alt="image1" width="500">
<img src="https://github.com/user-attachments/assets/f61a12e6-f3c2-47ae-8788-818a5a488459" alt="image2" width="500"> <br>

Clouds were made using Shader Graph. After creating the shader, it was added to a material and placed in the scene to make a cloudy sky effect.<br>
<img src="https://github.com/user-attachments/assets/580d3176-6d48-4cac-92fe-c74a5e5cc937" alt="image3" width="500">

ShaderGraph example: (Custom Skybox & Clouds ShaderGraph)<br>
<img src="https://github.com/user-attachments/assets/a6329ccb-3688-495f-a0b0-dea214cdc74b" alt="image4" width="500">
<img src="https://github.com/user-attachments/assets/97e9e5d3-2052-48ea-a8ac-fcc6e27b6e15" alt="image5" width="500">

### Lights
-	Street lights turn on automatically at night.
-	When the sun rises, the street lights turn off
<img src="https://github.com/user-attachments/assets/cafdc836-f0fe-41d5-94c5-606aa3aba158" alt="image6" width="500">
<img src="https://github.com/user-attachments/assets/a5796df1-b216-40c8-91eb-a96b5331f7ac" alt="image7" width="500">

### Rabbit Behavior
Rabbits react when the player gets close:
-	If you come near, they start running away with an animation and turn in the direction they escape.
-	When you move away, they stop and go back to their idle animation.
<img src="https://github.com/user-attachments/assets/9c713cfd-a9b0-47c2-aeb5-b9eafb2bd36a" alt="image8" width="500">
<img src="https://github.com/user-attachments/assets/875892d1-55bd-48dc-9862-281f78e12264" alt="image9" width="500">

### Interactable Cloud System
There are two rain clouds in the scene. Each cloud is made from three round shapes to look like a cartoon-style cloud. The rain effect is created using a particle system that shows falling raindrops.
-	The first cloud is always raining, and you can move it by interacting with it.
-	The second cloud starts raining only when you touch it, and you can move it as well.
<img src="https://github.com/user-attachments/assets/3bbffa29-d13e-4eda-992b-19c3b4302da1" alt="image10" width="500">
<img src="https://github.com/user-attachments/assets/3286a804-6eee-4ce4-baee-0b3a5387686d" alt="image11" width="500">
<img src="https://github.com/user-attachments/assets/f6747d26-e77b-4663-bfde-c0ae60903b02" alt="image12" width="500">

___

How to Download a Unity Project from GitHub
Click the green "Code" button.
You have two options:
- Click "Download ZIP" to download the project as a zip file.
- Or copy the link and use this command in your terminal:

git clone https://github.com/username/project-name.git

Open Unity Hub.
Click "Add" and select the folder you downloaded.
Open the project with the correct Unity version.
Note: Unity will create the missing folders (like Library) when opening the project.
