from flask import Flask, request, jsonify
import pickle
import numpy as np

app = Flask(__name__)

reg_model = pickle.load(open("models/regression.pkl", "rb"))
clf_model = pickle.load(open("models/classification.pkl", "rb"))

@app.route("/ml/predict-duration", methods=["POST"])
def predict_duration():
    data = request.json
    features = np.array([[data["category"], data["estimated"], data["priority"]]])
    result = reg_model.predict(features)
    return jsonify({"predicted_duration": float(result[0])})

@app.route("/ml/risk-score", methods=["POST"])
def risk_score():
    data = request.json
    features = np.array([[data["category"], data["estimated"], data["priority"]]])
    prob = clf_model.predict_proba(features)[0][0]
    return jsonify({"risk_score": float(prob * 100)})

app.run(port=5000)