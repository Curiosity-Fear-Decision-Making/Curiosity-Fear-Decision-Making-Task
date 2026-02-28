# Curiosity-Fear-Decision-Making Task

![Task Demo](https://github.com/Curiosity-Fear-Decision-Making/Curiosity-Fear-Decision-Making-Task/blob/main/task_demo.gif)

---
## Overview

This repository contains the **Curiosity-Fear-Decision-Making Task**, a Unity-based experimental paradigm implemented in **C#**, designed to investigate the interaction between curiosity, fear, and decision-making processes.

The task supports:

- Behavioral data collection  
- Real-time Eye-tracking integration  
- Experimental control for laboratory settings  
- Unity-based stimulus presentation and animation  

The task is compatible with **EyeLink 1000 Plus** and is designed for research applications in cognitive neuroscience and decision science.

---

## System Requirements

- **Unity Version:** 6000.0.62f1  
- **Eye Tracker:** EyeLink 1000 Plus  
- **Programming Language:** C#  
- **Recommended OS:** Windows (required for full EyeLink integration)

---
## EyeLink Integration

This project integrates EyeLink 1000 Plus via:

- Native DLL interface  
- Custom calibration window  
- Real-time gaze recording  
- Experimental event synchronization

## Project Structure

```
Assets/
├── ELMain.cs                  → Main EyeLink controller (initialization, calibration, recording control)
├── EyelinkWindow.cs           → EyeLink calibration window interface and display management
├── script_main.cs             → Core task controller (trial flow, logic, stimulus control, data logging)
├── script_menu.cs             → Experiment start menu and configuration interface
├── script_utility.cs          → Shared helper classes and utility functions
├── script_circle_pregressbar.cs → Circular progress bar UI component
├── script_draw_sector.cs      → Circular background / risk sector rendering
├── script_scale_animation.cs  → UI label scaling and animation controller
└── Plugins/                   → EyeLink SDK DLLs and required third-party libraries
```

## Data Output

The task logs:
- Behavioral responses  
- Trial timing  
- Eye-tracking synchronization markers  
- Experimental condition parameters  

---
