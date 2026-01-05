# Simple Byte XOR (Stream Utility)

A lightweight C# implementation of a byte-stream encryption algorithm using XOR logic. This tool is designed for speed and simplicity, making it ideal for educational purposes, key generation, or as a single layer in a multi-stage encryption pipeline.

---

## Key Features
* **Stream Functionality:** Processes data at the byte level, making it suitable for varying input sizes.
* **Lightweight XOR Logic:** High-speed execution with minimal computational overhead.
* **Modular Design:** Can be easily integrated into a larger encryption "pipe" or used as a secondary key-shuffling mechanism.

---

## Usage & Integration
This tool encrypts and decrypts text blocks (paragraphs) using a symmetric XOR approach. 

### Implementation Ideas:
1. **Pipeline Encryption:** Run this as a pre-processing step before applying heavier algorithms like AES.
2. **Key Obfuscation:** Use the stream to "scramble" a master key before it is stored or hashed.
3. **Data Masking:** Quick masking of non-sensitive logs or telemetry.

---

## Build & Install
* **Environment:** C# Console-Mode Project.
* **Requirements:** Visual Studio 2022 or higher.
* **Compilation:** 1. Open the `.sln` file in Visual Studio.
  2. Set the build configuration to **Release**.
  3. Build the solution to generate the executable.

---

## Security Warning
**Educational Use Only.** This algorithm is a lightweight stream function and is **not** cryptographically secure on its own. 
* **Weak Avalanche Effect:** Small changes in the input do not result in significant changes in the output.
* **Vulnerability:** Without being paired with more robust algorithms (like those found in **PassTwo**), it is susceptible to frequency analysis and known-plaintext attacks.

---

## 📝 Example
```text
Input:  "Hello World"
Action: XOR Stream Pass
Output: [Encrypted Byte Array]
Action: XOR Stream Pass (Same Key)
Result: "Hello World"
