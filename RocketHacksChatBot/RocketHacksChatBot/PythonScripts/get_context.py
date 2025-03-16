import sys
from langchain.embeddings import HuggingFaceEmbeddings
from langchain.vectorstores import FAISS
import io

def get_context_from_prompt(prompt, db_path="vectorDb", embedding_model="sentence-transformers/all-MiniLM-L12-v2", k=4):
    embeddings = HuggingFaceEmbeddings(model_name=embedding_model)
    db = FAISS.load_local(db_path, embeddings, allow_dangerous_deserialization=True) #Be very careful when using this flag.

    docs = db.similarity_search(prompt, k=k)
    context = [doc.page_content for doc in docs]
    return context

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python get_context.py <prompt>")
        sys.exit(1)

    prompt = sys.argv[1]
    context = get_context_from_prompt(prompt)
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
    
    for doc in context:
        print(doc)