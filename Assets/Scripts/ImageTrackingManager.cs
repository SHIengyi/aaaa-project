using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingManager : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    [SerializeField] private List<string> m_ImageNames;

    [SerializeField] private List<GameObject> m_TrackedImagePrefabs;

    private Dictionary<ARTrackedImage, GameObject> m_TrackedImagePrefabInstances = new();

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Added
        foreach (var newImage in eventArgs.added)
        {
            for (int i = 0; i < m_ImageNames.Count; i++) 
            {
                if (newImage.referenceImage.name.Equals(m_ImageNames[i])) 
                {
                    var prefabInstance = Instantiate(m_TrackedImagePrefabs[i], newImage.transform.position, newImage.transform.rotation);
                    m_TrackedImagePrefabInstances.Add(newImage, prefabInstance);
                }
            }
        }

        // Updated
        foreach (var updatedImage in eventArgs.updated)
        {
            if (m_TrackedImagePrefabInstances.ContainsKey(updatedImage)) 
            {
                var prefabInstance = m_TrackedImagePrefabInstances[updatedImage];
                prefabInstance.transform.SetPositionAndRotation(updatedImage.transform.position, updatedImage.transform.rotation);
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            if (m_TrackedImagePrefabInstances.ContainsKey(removedImage)) 
            {
                Destroy(m_TrackedImagePrefabInstances[removedImage]);
                m_TrackedImagePrefabInstances.Remove(removedImage);
            }
        }
    }
}
