# 📊 HOW TO USE THIS RESOURCE LIST

Instead of randomly watching or reading, go subject-by-subject. Master one area before moving on.

Each section contains:

✔️ Books
✔️ Courses
✔️ Videos
✔️ Tutorials/Articles
✔️ Example GitHub projects

---

# ✅ 1. FORMAL LANGUAGES & GRAMMAR (Foundation for Parsing)

## 📚 Books

* **“Introduction to the Theory of Computation” — Michael Sipser**
  *Covers formal languages, grammars, automata*
  [https://amzn.to/45z5Lqu](https://amzn.to/45z5Lqu)

* **“Compilers: Principles, Techniques & Tools” (the Dragon Book)**
  by Aho, Lam, Sethi, Ullman
  [https://amzn.to/3U2CgS7](https://amzn.to/3U2CgS7)

## 🎥 YouTube Series

* *The Dragon Book — Lexing & Parsing Concepts*
  (search: **"Dragon Book compiler lectures"**)

* Neso Academy — **Compiler Design Playlist**
  [https://www.youtube.com/playlist?list=PLBlnK6fEyqRjoG6aJ4N3J3uGPKPQmq8vp](https://www.youtube.com/playlist?list=PLBlnK6fEyqRjoG6aJ4N3J3uGPKPQmq8vp)

## 📝 Articles

* *Let’s Build A Compiler* series (by Jack Crenshaw)
  [https://compilers.iecc.com/crenshaw/](https://compilers.iecc.com/crenshaw/)

## 🧠 Concepts to Master

👉 EBNF, CFGs, terminals/nonterminals, recursion, left recursion, precedence, ambiguous grammars

---

# ✅ 2. PARSERS & INTERPRETERS (Core Skill)

## 📚 Books

* **“Crafting Interpreters” — Robert Nystrom** (free online)
  [https://craftinginterpreters.com/](https://craftinginterpreters.com/)

  *Builds a real language from scratch with Lexer → Parser → AST → Interpreter*

## 🎥 YouTube

* **Let’s Build a Compiler — by Nick Robert**
  [https://www.youtube.com/watch?v=HxaD_trXwRE](https://www.youtube.com/watch?v=HxaD_trXwRE)

## 🛠 Example Projects

* *Part-1 of “Build Your Own JavaScript Engine”*
  [https://github.com/michaelficarra/Esprima](https://github.com/michaelficarra/Esprima)

* *A full simple Lisp interpreter in C#*
  [https://github.com/AArnott/NLisp](https://github.com/AArnott/NLisp)

---

# ✅ 3. ABSTRACT SYNTAX TREES & LANGUAGE DESIGN

## 🔗 Guided Resources

* “Crafting Interpreters” — AST chapters (linked above)
* *Parser combinators in C#* for expression trees
  [https://github.com/scottksmith95/Passable](https://github.com/scottksmith95/Passable)

## 💡 Articles

* *AST is not parse tree — why?*
  [https://dev.to/dstucke/difference-between-parse-trees-and-abstract-syntax-trees-3ce5](https://dev.to/dstucke/difference-between-parse-trees-and-abstract-syntax-trees-3ce5)

---

# ✅ 4. DATABASE THEORY & RELATIONAL ALGEBRA

## 📚 Books

### 🔹 **Relational Algebra + Semantics**

* **“Database System Concepts” — Silberschatz, Korth, Sudarshan**
  [https://db-book.com/](https://db-book.com/)

* **“Fundamentals of Database Systems” — Elmasri & Navathe**
  [https://amzn.to/2W3nBsP](https://amzn.to/2W3nBsP)

These books explain relational algebra, query processing, and set/bag semantics.

## ⚡ Free PDF / Slides

* Query Processing & Optimization Slides
  [https://ssyu.im.ncnu.edu.tw/course/CSDB/chap12.pdf](https://ssyu.im.ncnu.edu.tw/course/CSDB/chap12.pdf)

---

# ✅ 5. DATABASE SYSTEM ARCHITECTURE

You *must* read this paper:

## 📄 **“The Architecture of a Database System”**

[https://dsf.berkeley.edu/papers/fntdb07-architecture.pdf](https://dsf.berkeley.edu/papers/fntdb07-architecture.pdf)

This explains:

✔ Parser → Planner → Optimizer → Executor
✔ Why query plans look like trees
✔ The role of cost models

---

# ✅ 6. QUERY PROCESSING & EXECUTION

## 🎥 YouTube

* **Database Internals Explained** — by Tech Dummies
  [https://www.youtube.com/watch?v=6N8dovqt0_E](https://www.youtube.com/watch?v=6N8dovqt0_E)

* **How SQL Engines Work** — by Coding Tech
  [https://www.youtube.com/watch?v=HPoWxbabO9o](https://www.youtube.com/watch?v=HPoWxbabO9o)

## 🧪 Example Engines

* **Apache Calcite (Java)** — production-level planner + optimizer
  [https://github.com/apache/calcite](https://github.com/apache/calcite)

* **TinyDB / Simple Query Engine in Python**
  [https://github.com/nushell/shim-datafusion](https://github.com/nushell/shim-datafusion)

Both are great to read and compare design decisions.

---

# ✅ 7. QUERY OPTIMIZATION

## 📚 Book Chapters

* Cost estimation
* Join reordering
* Predicate pushdown

From:

* Database System Concepts
* Fundamentals of Database Systems

## 📝 Articles

* *Rule-based vs Cost-based Optimization*
  [https://medium.com/@mukeshp/understanding-query-optimizer-in-sql-server-16be3ed2f806](https://medium.com/@mukeshp/understanding-query-optimizer-in-sql-server-16be3ed2f806)

---

# ✅ 8. EXECUTION ENGINE & ITERATOR MODELS

## 📄 Articles

* *Volcano (iterator) style execution*
  [https://dbepedia.com/volcano-iterator-model/](https://dbepedia.com/volcano-iterator-model/)

## 🔬 Papers

* *“Query Execution Techniques”* — PDF slides
  [https://www.slideshare.net/mihirpatel23/sql-query-execution-algorithms-and-optimizer](https://www.slideshare.net/mihirpatel23/sql-query-execution-algorithms-and-optimizer)

---

# ✅ 9. PRACTICAL IMPLEMENTATION RESOURCES

## 🎥 YouTube

* *Build a SQL Engine (SQLite internals talk)*
  [https://www.youtube.com/watch?v=4aXo3mGblw0](https://www.youtube.com/watch?v=4aXo3mGblw0)

* *DuckDB Architecture Overview*
  [https://www.youtube.com/watch?v=YTpu0t3-DmQ](https://www.youtube.com/watch?v=YTpu0t3-DmQ)

## 📦 Example Minimal Engines

* **minisql** — tiny SQL parser/engine (C++)
  [https://github.com/tylerneylon/minisql](https://github.com/tylerneylon/minisql)

* **sqlparser** (Rust)
  [https://github.com/sqlparser/sqlparser-rust](https://github.com/sqlparser/sqlparser-rust)

* **Relational Algebra Engine (C#)**
  [https://github.com/mangeshbendre/rasql](https://github.com/mangeshbendre/rasql)

---

# 📌 ONLINE & FREE COURSES

### 🔹 Coursera

* *Databases: Relational Databases and SQL*
  [https://www.coursera.org/specializations/databases](https://www.coursera.org/specializations/databases)

### 🔹 edX

* *Introduction to Database Management*
  [https://www.edx.org/course/introduction-to-databases](https://www.edx.org/course/introduction-to-databases)

### 🔹 YouTube Playlists

* *Database Systems (MIT / Stanford lectures)*
  Search: **“Database Systems MIT” / “Database Systems Stanford”**

---

# 📚 OPTIONAL BUT HIGHLY VALUABLE

## 📘 **Database Internals — Alex Petrov**

Detailed behind-the-scenes architecture
[https://amzn.to/3tKjOVc](https://amzn.to/3tKjOVc)

## 📘 **High-Performance Browser Networking** — Ilya Grigorik

Network/execution patterns (useful for query exec)
[https://hpbn.co/](https://hpbn.co/)

---

# 📌 YOUR STUDY PATH (Roadmap)

If you ask:

> What order should I learn these?

Here’s a **progressive learning plan**:

1️⃣ Formal Grammars & Parsing
2️⃣ ASTs & Interpreter Architecture
3️⃣ Relational Algebra theory
4️⃣ Query execution & iterator models
5️⃣ Semantic analysis
6️⃣ Logical plans and rewriting
7️⃣ SQL → RA translation
8️⃣ Optimizer basics
9️⃣ Implement a working engine

---

# 🧠 A KEY TIP

Do **NOT** start coding until you can:

* Define grammar in EBNF
* Hand-construct ASTs for expressions
* Walk AST manually and produce results

This is the moment many people fail.

