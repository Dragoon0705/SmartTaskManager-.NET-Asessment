import pandas as pd
from sklearn.preprocessing import LabelEncoder

def load_and_clean():
    df = pd.read_csv("data/tasks.csv")

    # Encode categorical columns
    le_category = LabelEncoder()
    le_priority = LabelEncoder()
    le_day = LabelEncoder()

    df["TaskCategory"] = le_category.fit_transform(df["TaskCategory"])
    df["Priority"] = le_priority.fit_transform(df["Priority"])
    df["DayOfWeek"] = le_day.fit_transform(df["DayOfWeek"])

    return df, le_category, le_priority, le_day